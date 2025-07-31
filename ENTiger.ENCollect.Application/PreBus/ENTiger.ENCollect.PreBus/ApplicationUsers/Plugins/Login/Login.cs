using ENCollect.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.ApplicationUsersModule.LoginApplicationUsersPlugins
{
    public partial class Login : FlexiBusinessRuleBase, IFlexiBusinessRule<LoginDataPacket>
    {
        public override string Id { get; set; } = "3a12e6182f8092dacf576608030dee27";
        public override string FriendlyName { get; set; } = "Login";

        private readonly ILogger<Login> _logger;
        private readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly AuthSettings _authSettings;
        private readonly PasswordSettings _passwordSettings;
        private readonly LockoutSettings _lockoutSettings;
        private readonly IApiHelper _apiHelper;
        protected string EventCondition = "";
        protected AuditEventData _auditData;
        LoginDetailsHistory loginDetailsHistory;
        protected readonly IFlexServiceBusBridge _flexServiceBusBridge;
        protected readonly IUserUtility _userUtility;
        private readonly LoginSettings _loginSettings;

        public Login(ILogger<Login> logger, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings
                , IOptions<PasswordSettings> passwordSettings, IOptions<LockoutSettings> lockoutSettings
                , IApiHelper apiHelper, IFlexServiceBusBridge flexServiceBusBridge, IUserUtility userUtility, IOptions<LoginSettings> loginSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _passwordSettings = passwordSettings.Value;
            _lockoutSettings = lockoutSettings.Value;
            _apiHelper = apiHelper;
            _flexServiceBusBridge = flexServiceBusBridge;
            _userUtility = userUtility;
            _loginSettings = loginSettings.Value;
        }

        public virtual async Task Validate(LoginDataPacket packet)
        {
            var notificationDays = _passwordSettings.ExpiryNotificationInDays;
            var expiryDays = _passwordSettings.ExpiryInDays;
            var aesGcmCrypto = new AesGcmCrypto();

            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext(); //do not remove this line
            string tenantId = _flexAppContext.TenantId;
            _logger.LogInformation("Login Start | TenantId: {TenantId}", tenantId);

            if (packet.HasError) return; // Early exit if packet has errors
            
            try
            {
                string key = packet.Key;
                var model = packet.Dto;
                string? userName = aesGcmCrypto.Decrypt(model.UserName, Encoding.UTF8.GetBytes(key));
                string password = aesGcmCrypto.Decrypt(model.Password, Encoding.UTF8.GetBytes(key));

                // Check if user exists
                var appUser = await GetUserAsync(userName);
                if (appUser == null)
                {
                    packet.AddError("Error", "User does not exist or is not confirmed.");
                    return;
                }

                // Check if FOS login from web is allowed for the current user
                bool isFosLoginAllowed = await IsFosLoginAllowedInWeb(appUser);
                if (!isFosLoginAllowed)
                {
                    // Block login and show error message
                    packet.AddError("Error", "FOS users are not allowed to log in from the web application.");
                    return;
                }

                // Create a new login details history record
                loginDetailsHistory = new LoginDetailsHistory
                {
                    UserId = appUser.Id
                };

                // Handle user lock status
                if (await IsUserLocked(appUser))
                {
                    // Log if the user is locked
                    _logger.LogInformation("Login : User Locked | UserName - " + userName);
                    loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                    loginDetailsHistory.LoginStatus = $"Your account is locked due to {_lockoutSettings.Attempts} failed login attempts. Wait {_lockoutSettings.EndInHours} hours to try again or reset your password to regain access immediately.";

                    packet.AddError("Error", loginDetailsHistory.LoginStatus);
                }
                else                
                {
                    var permissions = await _userUtility.GetUserPermissions(appUser, packet.Dto);

                    var authApiUrl = _authSettings.AuthUrl;

                    // Handling username when it contains a mobile number (without domain)
                    if (!userName.Contains("."))
                    {
                        userName = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                                .Where(x => x.PrimaryMobileNumber == userName)
                                                .Select(s => s.PrimaryEMail)
                                                .FirstOrDefaultAsync();
                        password = _authSettings.Password + password;
                    }

                    var claims = await _userUtility.GetClaims(packet.Dto);
                    var data = new
                    {
                        UserName = $"{tenantId}_{userName}",
                        Password = password,
                        Permissions = permissions.Select(p => p.Name).ToList(),
                        Claims = claims
                    };
                    string jsonPayload = JsonConvert.SerializeObject(data);
                    var response = await _apiHelper.SendRequestAsync(jsonPayload, $"{authApiUrl}/api/AccountAPI/login", HttpMethod.Post);
                    if (!response.IsSuccessStatusCode)
                    {
                        LoginValidationDto value = JsonConvert.DeserializeObject<LoginValidationDto>(response.Content.ReadAsStringAsync().Result.ToString());
                        if (value.error.Contains("Locked"))
                        {
                            // Lock user after failed login attempts
                            loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                            loginDetailsHistory.Remarks = value.error_description;
                            packet.AddError("Error", value.error_description);

                            appUser.IsLocked = true;
                            appUser.LockedDateTime = DateTime.Now;
                            appUser.SetAddedOrModified();
                            _repoFactory.GetRepo().InsertOrUpdate(appUser);
                            await _repoFactory.GetRepo().SaveAsync();
                        }
                        else
                        {
                            loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                            loginDetailsHistory.Remarks = value.error_description;
                            packet.AddError("Error", value.error_description);
                        }
                    }
                    else
                    {
                        // Handle successful login response
                        var result = response.Content.ReadAsStringAsync().Result;
                        var tokenDetails = JsonConvert.DeserializeObject<AuthToken>(result);
                        if (tokenDetails != null)
                        {                            
                            var passwordLastUpdated = tokenDetails.password_last_updated;
                            if(passwordLastUpdated != null && DateTime.Now.Date >= passwordLastUpdated?.AddDays(expiryDays))                            
                            {
                                loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                                loginDetailsHistory.Remarks = "Your password has expired. Please click on Forgot Password";
                                packet.AddError("Error", "Your password has expired. Please click on Forgot Password? link on the Login page to create your new password.");
                            }
                            else
                            {
                                loginDetailsHistory.LoginStatus = LoginStatusEnum.Success.Value;
                                var days = (passwordLastUpdated?.Date.AddDays(expiryDays) - DateTime.Now.Date)?.Days;
                                TokenDto token = new TokenDto
                                {
                                    access_token = tokenDetails.access_token,
                                    message = days <= notificationDays ? "Your password is about to expire in " + days + " days. Please change your password using profile setting screen." : string.Empty
                                };
                                packet.token = token;
                            }
                        }
                    }
                }

                await SaveLoginDetailsHistory(loginDetailsHistory);

                await GenerateAndSendAuditEventAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Login : " + ex);
                packet.AddError("Error", "Error occured during login, please contact administrator");
            }            
            _logger.LogInformation("Login  : End");
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

        private async Task<ApplicationUser?> GetUserAsync(string userName)
        {
            var repo = _repoFactory.GetRepo();
            ApplicationUser? appUser = null;

            if (userName.Contains("."))
            {
                appUser = await repo.FindAll<ApplicationUser>().Where(x => x.PrimaryEMail == userName && !x.IsDeleted).FirstOrDefaultAsync();
            }
            else
            {
                appUser = await repo.FindAll<ApplicationUser>().Where(x => x.PrimaryMobileNumber == userName && !x.IsDeleted).FirstOrDefaultAsync();
            }
            return appUser;
        }

        // Private method to check if the user is locked
        private async Task<bool> IsUserLocked(ApplicationUser appUser)
        {
            // Check if the user is locked and update the lock status if necessary
            if (appUser.IsLocked && appUser.LockedDateTime != null && DateTime.Now > appUser.LockedDateTime.Value.AddHours(_lockoutSettings.EndInHours))
            {
                appUser.IsLocked = false;
                appUser.LockedDateTime = null;
                appUser.SetAddedOrModified();
                _repoFactory.GetRepo().InsertOrUpdate(appUser);
                await _repoFactory.GetRepo().SaveAsync();
                return false; // User is no longer locked
            }
            return appUser.IsLocked; // User remains locked
        }

        // Private method to save login details history
        private async Task SaveLoginDetailsHistory(LoginDetailsHistory loginDetailsHistory)
        {
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
        }
        /// <summary>
        /// Checks whether FOS user is allowed to log in from the web based on login settings.
        /// </summary>
        /// <param name="appUser">The application user attempting to log in.</param>
        /// <returns>True if allowed, false if blocked by configuration.</returns>
        private async Task<bool> IsFosLoginAllowedInWeb(ApplicationUser appUser)
        {
            // Check if FOS login from web is disabled in settings
            if (!_loginSettings.IsFosWebLoginAllowed)
            {
                // If the user is of type FOS and web login is disabled, deny access
                if (appUser.UserType == UserTypeEnum.FOS.ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
