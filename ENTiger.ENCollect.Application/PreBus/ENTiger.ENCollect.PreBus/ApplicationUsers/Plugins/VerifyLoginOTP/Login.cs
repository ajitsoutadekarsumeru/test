using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule.VerifyLoginOTPApplicationUsersPlugins
{
    public partial class Login : FlexiBusinessRuleBase, IFlexiBusinessRule<VerifyLoginOTPDataPacket>
    {
        public override string Id { get; set; } = "3a134c68af855f9e8383630416d6fae3";
        public override string FriendlyName { get; set; } = "Login";

        protected readonly ILogger<Login> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly AuthSettings _authSettings;
        private string _authUrl;
        private readonly IApiHelper _apiHelper;
        protected string EventCondition = "";
        protected AuditEventData _auditData;
        LoginDetailsHistory loginDetailsHistory;
        protected readonly IFlexServiceBusBridge _flexServiceBusBridge;
        protected readonly IUserUtility _userUtility;

        public Login(ILogger<Login> logger, IRepoFactory repoFactory,IOptions<AuthSettings> authSettings, IApiHelper apiHelper, IFlexServiceBusBridge flexServiceBusBridge, IUserUtility userUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _authUrl = _authSettings.AuthUrl;
            _apiHelper = apiHelper;
            _flexServiceBusBridge = flexServiceBusBridge;
            _userUtility = userUtility;
        }

        public virtual async Task Validate(VerifyLoginOTPDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();
            string tenantId = _flexAppContext.TenantId;

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            if (packet.HasError)    return;
                      
            string emailId = packet.Dto.emailId;

            ApplicationUser? appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                            .Where(x => x.PrimaryEMail == emailId && x.IsDeleted == false)
                                            .FirstOrDefaultAsync();
            if (appUser == null)
            {
                _logger.LogWarning("User not found with email: {Email}", emailId);
                packet.AddError("Error", "User not found.");
                return;
            }
            var permissions = await _userUtility.GetUserPermissions(appUser, packet.Dto);

            loginDetailsHistory = new LoginDetailsHistory();
            loginDetailsHistory.UserId = appUser.Id;

            var claims = await _userUtility.GetClaims(packet.Dto);

            var data = new
            {
                UserName = $"{tenantId}_{emailId}",
                Permissions = permissions.Select(x => x.Name).ToList()
            };

            string jsonPayload = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await _apiHelper.SendRequestAsync(jsonPayload, $"{_authUrl}/api/AccountAPI/adlogin", HttpMethod.Post);
            if (!response.IsSuccessStatusCode)
            {
                LoginValidationDto value = JsonConvert.DeserializeObject<LoginValidationDto>(response.Content.ReadAsStringAsync().Result.ToString());
                packet.AddError("Error", value.error_description);
                loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                loginDetailsHistory.Remarks = value.error_description;
            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    packet.OutputDto = JsonConvert.DeserializeObject<TokenDto>(result);
                    loginDetailsHistory.LoginStatus = LoginStatusEnum.Success.Value;
                }
            }                    
            loginDetailsHistory.SetAddedOrModified();
            _repoFactory.GetRepo().InsertOrUpdate(loginDetailsHistory);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(LoginDetailsHistory).Name, loginDetailsHistory.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(LoginDetailsHistory).Name, loginDetailsHistory.Id);
            }

            await GenerateAndSendAuditEventAsync();
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
    }
}
