using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ENCollect.Security;
using Sumeru.Flex;
using Microsoft.Extensions.Options;
using Elastic.Clients.Elasticsearch.MachineLearning;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.ApplicationUsersModule.MobileLoginApplicationUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateLoginAndSendLoginOTP : FlexiBusinessRuleBase, IFlexiBusinessRule<MobileLoginDataPacket>
    {
        public override string Id { get; set; } = "3a13383c4d8cdc968766b92021684d25";
        public override string FriendlyName { get; set; } = "ValidateLoginAndSendLoginOTP";

        protected readonly ILogger<ValidateLoginAndSendLoginOTP> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        private readonly ICustomUtility _customUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly AuthSettings _authSettings;
        private readonly NotificationSettings _notificationSettings;
        private readonly OtpSettings _otpSettings;
        private readonly PasswordSettings _passwordSettings;
        private readonly LockoutSettings _lockoutSettings;
        private readonly LoginSettings _loginSettings;
        private readonly IApiHelper _apiHelper;

        public ValidateLoginAndSendLoginOTP(ILogger<ValidateLoginAndSendLoginOTP> logger,
            IRepoFactory repoFactory
            , ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, IOptions<AuthSettings> authSettings, IOptions<NotificationSettings> notificationSettings, IOptions<OtpSettings> otpSettings, ICustomUtility customUtility
            , IOptions<PasswordSettings> passwordSettings, IOptions<LockoutSettings> lockoutSettings, IOptions<LoginSettings> loginSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _authSettings = authSettings.Value;
            _notificationSettings = notificationSettings.Value;
            _otpSettings = otpSettings.Value;
            _customUtility = customUtility;
            _passwordSettings = passwordSettings.Value;
            _lockoutSettings = lockoutSettings.Value;
            _loginSettings = loginSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Validate(MobileLoginDataPacket packet)
        {
            var notificationDays = _passwordSettings.ExpiryNotificationInDays;
            var expiryDays = _passwordSettings.ExpiryInDays;
            var aesGcmCrypto = new AesGcmCrypto();

            bool staticOTP = _otpSettings.StaticOtp.Enabled;
            string otp = _otpSettings.StaticOtp.Otp;

            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();
            string tenantId = _flexAppContext.TenantId;
            //If any validation fails, uncomment and use the below line of code to add error to the packet
            if (packet.HasError) return; // Early exit if packet has errors
                        
            _logger.LogInformation("ValidateLoginAndSendLoginOTP : Start | TenantId : " + tenantId);
            try
            {
                string key = packet.Key;
                var model = packet.Dto;
                string email = aesGcmCrypto.Decrypt(model.EmailId, Encoding.UTF8.GetBytes(key));
                string password = aesGcmCrypto.Decrypt(model.password, Encoding.UTF8.GetBytes(key));
                _logger.LogInformation("Decrypted emailid " + email);

                ApplicationUser? appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.PrimaryEMail == email && x.IsDeleted == false).FirstOrDefaultAsync();
                if (appUser == null)
                {
                    packet.AddError("Error", "User does not exist or is not confirmed.");
                    return;
                }

                // Check if Telecaller login from mobile is allowed for the current user
                bool isTelecallerMobileLoginAllowed = await IsTelecallerLoginAllowedInMobile(appUser);
                if (!isTelecallerMobileLoginAllowed)
                {
                    // Block login and show error message
                    packet.AddError("Error", "Telecaller users are not allowed to log in from the mobile application.");
                    return;
                }

                LoginDetailsHistory loginDetailsHistory = new LoginDetailsHistory();
                loginDetailsHistory.UserId = appUser.Id;

                if (appUser.IsLocked && appUser.LockedDateTime != null && DateTime.Now > appUser.LockedDateTime.Value.AddHours(_lockoutSettings.EndInHours))
                {
                    appUser.IsLocked = false;
                    appUser.LockedDateTime = null;
                    appUser.SetAddedOrModified();
                    _repoFactory.GetRepo().InsertOrUpdate(appUser);
                    await _repoFactory.GetRepo().SaveAsync();
                }
                if (!appUser.IsLocked)
                {
                    var authApiUrl = _authSettings.AuthUrl;
                    _logger.LogInformation("ValidateLoginAndSendLoginOTP : AuthURL : " + authApiUrl);

                    var data = new
                    {
                        emailid = tenantId + "_" + email,
                        password = password
                    };
                    string jsonPayload = JsonConvert.SerializeObject(data);
                    _logger.LogInformation("ValidateLoginAndSendLoginOTP : JSON : " + JsonConvert.SerializeObject(jsonPayload));

                    var validatelogin = await _apiHelper.SendRequestAsync(jsonPayload, authApiUrl + "/api/AccountAPI/ValidateLogin",HttpMethod.Post);
                    _logger.LogInformation("ValidateLoginAndSendLoginOTP : StatusCode : " + validatelogin.StatusCode);
                    if (!validatelogin.IsSuccessStatusCode)
                    {
                        LoginValidationDto value = JsonConvert.DeserializeObject<LoginValidationDto>(validatelogin.Content.ReadAsStringAsync().Result.ToString());
                        if (value.error.Contains("Locked"))
                        {
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
                        var shortcode = (staticOTP == true) ? otp : _customUtility.GenerateRandomCode();
                        _logger.LogInformation("ValidateLoginAndSendLoginOTP : ShortCode : " + shortcode);
                        try
                        {
                            UserVerificationCodes usercode = new UserVerificationCodes();
                            usercode.AddLoginUserVerificationCode(shortcode, appUser.Id);

                            _repoFactory.GetRepo().InsertOrUpdate(usercode);
                            int record = await _repoFactory.GetRepo().SaveAsync();

                            var SMSSignature = _notificationSettings.SmsSignature;
                            var loginOTPExpiredTime = Convert.ToString(_otpSettings.Expiry.LoginOtpInMins);
                            try
                            {
                                var otpDto = new SendLoginOTPDto
                                {
                                    Otp = shortcode,
                                    ExpiryTime = loginOTPExpiredTime,
                                    Signature = SMSSignature,
                                    FirstName = appUser.FirstName,
                                    LastName = appUser.LastName,
                                };
                                otpDto.SetAppContext(packet.Dto.GetAppContext());
                                var messageTemplate = _messageTemplateFactory.LoginOTPTemplate(otpDto);

                                _logger.LogInformation("SendOTPForLoginService : SendSMS : MobileNumber - " + appUser.PrimaryMobileNumber);
                                await _smsUtility.SendSMS(appUser.PrimaryMobileNumber, messageTemplate.SMSMessage, packet.Dto.GetAppContext().TenantId);


                                _logger.LogInformation("SendOTPForLoginService : SendMail : Email - " + appUser.PrimaryEMail);
                                await _emailUtility.SendEmailAsync(appUser.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, packet.Dto.GetAppContext().TenantId);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("Exception in SendOTPForLoginService " + ex);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("Exception in UserVerificationCodes Generation " + ex);
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("Login : User Locked | UserName - " + email);
                    loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                    loginDetailsHistory.LoginStatus = $"Your account is locked due to {_lockoutSettings.Attempts} failed login attempts. Wait {_lockoutSettings.EndInHours} hours to try again or reset your password to regain access immediately.";

                    packet.AddError("Error", loginDetailsHistory.LoginStatus);
                }

                //save loginDetailHistory
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
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception in ValidateLoginAndSendLoginOTP : " + ex);
                packet.AddError("Error", "Something went Wrong Please try again");
            }
            _logger.LogInformation("ValidateLoginAndSendLoginOTP : End");
            
            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }

        /// <summary>
        /// Checks whether Telecaller user is allowed to log in from the mobile application based on login settings.
        /// </summary>
        /// <param name="appUser">The application user attempting to log in.</param>
        /// <returns>True if allowed, false if blocked by configuration.</returns>
        private async Task<bool> IsTelecallerLoginAllowedInMobile(ApplicationUser appUser)
        {
            // Check if Telecaller login from mobile is disabled in settings
            if (!_loginSettings.IsTelecallerMobileLoginAllowed)
            {
                // If the user is a Telecaller and mobile login is disabled, deny access
                if (appUser.UserType == UserTypeEnum.Telecaller.ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
