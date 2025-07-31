using System.Text;
using Microsoft.Extensions.Logging;
using ENCollect.Security;
using Sumeru.Flex;
using Microsoft.Extensions.Options;
using Elastic.Clients.Elasticsearch.MachineLearning;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.ApplicationUsersModule.MobileResetPasswordApplicationUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ResetPassword : FlexiBusinessRuleBase, IFlexiBusinessRule<MobileResetPasswordDataPacket>
    {
        public override string Id { get; set; } = "3a1343f31b06e7a320269d3d6ff6f923";
        public override string FriendlyName { get; set; } = "ResetPassword";

        protected readonly ILogger<ResetPassword> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly AuthSettings _authSettings;
        private readonly OtpSettings _otpSettings;
        private readonly IApiHelper _apiHelper;

        public ResetPassword(ILogger<ResetPassword> logger, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings
            , IOptions<OtpSettings> otpSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _otpSettings = otpSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Validate(MobileResetPasswordDataPacket packet)
        {
            string authUrl = _authSettings.AuthUrl;
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();

            string tenantId = _flexAppContext.TenantId;

            if (!packet.HasError)
            {
                try
                {
                    string key = packet.Key;
                    string password = string.Empty;
                    string confirmPassword = string.Empty;
                    string code = string.Empty;
                    var aesGcmCrypto = new AesGcmCrypto();
                    var aesGcmKey = Encoding.UTF8.GetBytes(key);

                    password = aesGcmCrypto.Decrypt(packet.Dto.Password, aesGcmKey);
                    confirmPassword = aesGcmCrypto.Decrypt(packet.Dto.ConfirmPassword, aesGcmKey);
                    code = aesGcmCrypto.Decrypt(packet.Dto.Code, aesGcmKey);

                    if (password != confirmPassword)
                    {
                        packet.AddError("Error", "The password and confirmation password do not match.");
                    }

                    ResetPasswordDto model = new ResetPasswordDto()
                    {
                        Email = packet.Dto.Email,
                        Password = password,
                        ConfirmPassword = confirmPassword,
                        Code = code,
                    };

                    ApplicationUser? appUser;
                    UserVerificationCodes? verificationCodes;
                    string shortcode = model.Code;

                    string authapiUrl = authUrl;

                    string verifycode = string.Empty;

                    var data = new
                    {
                        email = tenantId + "_" + model.Email,
                        password = _authSettings.Password,
                        confirmPassword = _authSettings.ConfirmPassword,
                        firstName = "test",
                        middleName = "",
                        lastName = ""
                    };
                    string jsonPayload = JsonConvert.SerializeObject(data);
                    var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);

                    string authUserId = await checkExistName.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(authUserId))
                    {
                        packet.AddError("Error", "Invalid User");
                    }
                    else
                    {
                        appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();

                        DateTimeOffset timeCheck = new DateTimeOffset();
                        Int32 forgotpwdexpirytime = _otpSettings.Expiry.ForgotPasswordOtpInMins;
                        string? userVerificationCodeTypeId = await _repoFactory.GetRepo().FindAll<UserVerificationCodeTypes>()
                                                                    .Where(i => string.Equals(i.Description, "forgotpassword"))
                                                                    .Select(i => i.Id)
                                                                    .FirstOrDefaultAsync();

                        timeCheck = DateTimeOffset.Now.AddMinutes(-forgotpwdexpirytime);
                        verificationCodes = await _repoFactory.GetRepo().FindAll<UserVerificationCodes>()
                                                        .Where(a => a.ShortVerificationCode == shortcode &&
                                                                    a.UserId == appUser.Id &&
                                                                    a.UserVerificationCodeTypeId == userVerificationCodeTypeId)
                                                        .OrderByDescending(v => v.Id)
                                                        .ThenByDescending(v => v.CreatedDate)
                                                        .FirstOrDefaultAsync();

                        if (verificationCodes != null)
                        {
                            verifycode = verificationCodes.VerificationCode;
                            var validateData = new
                            {
                                Email = tenantId + "_" + appUser.PrimaryEMail,
                                Password = model.Password,
                                NewPassword = model.Password
                            };
                            string validateDataJsonPayload = JsonConvert.SerializeObject(validateData);
                            var checkPasswordHistroy = await _apiHelper.SendRequestAsync(validateDataJsonPayload, authapiUrl + "/api/AccountAPI/CheckPasswordHistroy", HttpMethod.Post);

                            if (!checkPasswordHistroy.IsSuccessStatusCode)
                            {
                                packet.AddError("Error", checkPasswordHistroy.Content.ReadAsStringAsync().Result);
                            }
                            else
                            {
                                var dataReset = new
                                {
                                    Email = tenantId + "_" + appUser.PrimaryEMail,
                                    Code = @verifycode,
                                    Password = model.Password,
                                    ConfirmPassword = model.ConfirmPassword
                                };
                                string dataResetJsonPayload = JsonConvert.SerializeObject(dataReset);
                                var forgotPasswordresponse = await _apiHelper.SendRequestAsync(dataResetJsonPayload, authapiUrl + "/api/AccountAPI/ResetPassword", HttpMethod.Post);

                                if (!forgotPasswordresponse.IsSuccessStatusCode)
                                {
                                    packet.AddError("Error", "Something went wrong. Please try again after sometime");
                                }
                                else
                                {
                                    appUser.IsLocked = false;
                                    appUser.LockedDateTime = null;
                                    appUser.SetAddedOrModified();
                                    _repoFactory.GetRepo().InsertOrUpdate(appUser);
                                    int records = await _repoFactory.GetRepo().SaveAsync();
                                }
                            }
                        }
                        else
                        {
                            packet.AddError("Error", "Code does not match/expired");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex + ex?.Message + ex?.StackTrace + ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace;
                    _logger.LogInformation("ResetPassword : " + errorMsg);
                    packet.AddError("Error", ex);
                }
            }

            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
