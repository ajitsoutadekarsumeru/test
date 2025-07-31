using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;
using ENCollect.Security;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.ApplicationUsersModule.ADLoginApplicationUsersPlugins
{
    public partial class ADLogin : FlexiBusinessRuleBase, IFlexiBusinessRule<ADLoginDataPacket>
    {
        public override string Id { get; set; } = "3a1347a8b40c84e1882203e2df6e6be3";
        public override string FriendlyName { get; set; } = "ADLogin";

        protected readonly ILogger<ADLogin> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ADAuthProviderFactory _adAuthProviderFactory;
        string authUrl =  string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;
        protected string EventCondition = "";
        protected AuditEventData _auditData;
        LoginDetailsHistory loginDetailsHistory;
        protected readonly IFlexServiceBusBridge _flexServiceBusBridge;
        protected readonly IUserUtility _userUtility;

        public ADLogin(ILogger<ADLogin> logger, IRepoFactory repoFactory, ADAuthProviderFactory adAuthProviderFactory
                , IOptions<AuthSettings> authSettings, IApiHelper apiHelper, IFlexServiceBusBridge flexServiceBusBridge, 
                IUserUtility userUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _adAuthProviderFactory = adAuthProviderFactory;
            _authSettings = authSettings.Value;
            _apiHelper = apiHelper;
            _flexServiceBusBridge = flexServiceBusBridge;
            _userUtility = userUtility;
        }

        public virtual async Task Validate(ADLoginDataPacket packet)
        {
            authUrl = _authSettings.AuthUrl;
            // Initialize repository and application context
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();  // Keep this line

            string tenantId = _flexAppContext.TenantId;
            _logger.LogInformation($"ADLogin : Start | TenantId - {tenantId}");

            if (!packet.HasError)
            {
                try
                {
                    _logger.LogInformation($"ADLogin : JSON - {JsonConvert.SerializeObject(packet)}");

                    // Initialize variables
                    var model = packet.Dto;
                    var key = packet.Key;

                    // Decrypt values based on AESGCMEnabled flag
                    (string userName, string ADPassword) = DecryptValues(model, key);

                    // Prepare data for authentication
                    model.Password = ADPassword;
                    model.UserName = userName;

                    // Fetch user from repository
                    var appUser = await _repoFactory.GetRepo().FindAll<CompanyUser>().FirstOrDefaultAsync(x => x.DomainId == userName && !x.IsDeleted);

                    if (appUser != null)
                    {
                        loginDetailsHistory = new LoginDetailsHistory
                        {
                            UserId = appUser?.Id
                        };

                        var adAuthProvider = _adAuthProviderFactory.GetADAuthProvider("ldap");

                        // Authenticate user
                        bool isAuthenticated = await adAuthProvider.Authenticate(model);

                        if (isAuthenticated)
                        {
                            await HandleSuccessfulAuthentication(tenantId, appUser, loginDetailsHistory, packet);
                        }
                        else
                        {
                            HandleFailedAuthentication(loginDetailsHistory, packet);
                        }

                        // Save login details history
                        loginDetailsHistory.SetAddedOrModified();
                        _repoFactory.GetRepo().InsertOrUpdate(loginDetailsHistory);
                        int records = await _repoFactory.GetRepo().SaveAsync();

                        if (records > 0)
                        {
                            _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AgencyUser).Name, loginDetailsHistory.Id);
                        }
                        else
                        {
                            _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AgencyUser).Name, loginDetailsHistory.Id);
                        }

                        await GenerateAndSendAuditEventAsync();
                    }
                    else
                    {
                        packet.AddError("Error", "Invalid Username or Password");
                    }
                }
                catch (Exception ex)
                {
                   _logger.LogError($"{tenantId} - Exception in ADLogin : {ex.Message}");
                    packet.AddError("Error", "Invalid Username or Password");
                }
            }

            _logger.LogInformation("ADLogin : End");
        }

        private async Task GenerateAndSendAuditEventAsync()
        {
            string jsonPatch = JsonConvert.SerializeObject(loginDetailsHistory);

            _auditData = new AuditEventData(
                            EntityId: loginDetailsHistory?.UserId,
                            EntityType: AuditedEntityTypeEnum.Login.Value,
                            Operation: AuditOperationEnum.Add.Value,
                            JsonPatch: jsonPatch,
                            InitiatorId: _flexAppContext?.UserId,
                            TenantId: _flexAppContext?.TenantId,
                            ClientIP: _flexAppContext?.ClientIP
                        );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, new FlexServiceBusContextBridge(_flexServiceBusBridge));
        }

        // Helper method to decrypt values
        private (string userName, string ADPassword) DecryptValues(ADLoginDto model, string key)
        {
            var aesGcmCrypto = new AesGcmCrypto();
            var aesGcmKey = Encoding.UTF8.GetBytes(key);

            return (aesGcmCrypto.Decrypt(model.UserName, aesGcmKey), aesGcmCrypto.Decrypt(model.Password, aesGcmKey));
        }

        // Handle successful authentication
        private async Task HandleSuccessfulAuthentication(string tenantId, CompanyUser appUser, LoginDetailsHistory loginDetailsHistory, ADLoginDataPacket packet)
        {
            var permissions = await _userUtility.GetUserPermissions(appUser, packet.Dto);
            var adData = new
            {
                UserName = $"{tenantId}_{appUser.PrimaryEMail}",
                Permissions = permissions.Select(x => x.Name).ToList()
            };
            string authapiUrl = authUrl; // Consider refactoring this to fetch dynamically if needed
            _logger.LogInformation($"ADLogin : authapiUrl - {authapiUrl}");

            string jsonPayload = JsonConvert.SerializeObject(adData);
            var response = await _apiHelper.SendRequestAsync(jsonPayload, $"{authapiUrl}/api/AccountAPI/adlogin", HttpMethod.Post);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<LoginValidationDto>(errorContent);
                packet.AddError("Error", value.error_description);

                loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                loginDetailsHistory.Remarks = value.error_description;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                loginDetailsHistory.LoginStatus = LoginStatusEnum.Success.Value;
                packet.OutputDto = JsonConvert.DeserializeObject<TokenDto>(result);
            }
        }

        // Handle failed authentication
        private void HandleFailedAuthentication(LoginDetailsHistory loginDetailsHistory, ADLoginDataPacket packet)
        {
            loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
            loginDetailsHistory.Remarks = "Invalid Username or Password";
            packet.AddError("Error", "Invalid Username or Password");
        }       

    }
}
