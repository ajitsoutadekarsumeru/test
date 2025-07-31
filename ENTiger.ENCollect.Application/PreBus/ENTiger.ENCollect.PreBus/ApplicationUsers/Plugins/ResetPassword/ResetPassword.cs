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

namespace ENTiger.ENCollect.ApplicationUsersModule.ResetPasswordApplicationUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ResetPassword : FlexiBusinessRuleBase, IFlexiBusinessRule<ResetPasswordDataPacket>
    {
        public override string Id { get; set; } = "3a1343f2141db06fae0364b6c708294a";
        public override string FriendlyName { get; set; } = "ResetPassword";

        protected readonly ILogger<ResetPassword> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly AuthSettings _authSettings;
        private readonly OtpSettings _otpSettings;
        private readonly IApiHelper _apiHelper;
        public ResetPassword(ILogger<ResetPassword> logger, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings,
                                IOptions<OtpSettings> otpSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _otpSettings = otpSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Validate(ResetPasswordDataPacket packet)
        {
            _logger.LogInformation("ResetPassword : Start");
            string authUrl = _authSettings.AuthUrl;
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();

            string tenantId = _flexAppContext.TenantId;

            if (packet.HasError) return; // If there are any errors, no further processing is needed

            var aesGcmCrypto = new AesGcmCrypto();
            string key = packet.Key;

            var password = aesGcmCrypto.Decrypt(packet.Dto.Password, Encoding.UTF8.GetBytes(key));
            var confirmPassword = aesGcmCrypto.Decrypt(packet.Dto.ConfirmPassword, Encoding.UTF8.GetBytes(key));
            if (password != confirmPassword)
            {
                packet.AddError("Error", "The password and confirmation password do not match.");
                return;
            }
            ResetPasswordDto model = new ResetPasswordDto()
            {
                Email = packet.Dto.Email,
                Password = password,
                ConfirmPassword = confirmPassword,
                Code = packet.Dto.Code,
            };

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
                packet.AddError("Error", "No user found.");
            }
            else
            {
                ApplicationUser? appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();
                                                
                DateTimeOffset timeCheck = new DateTimeOffset();
                Int32 forgotpwdexpirytime = _otpSettings.Expiry.ForgotPasswordOtpInMins;

                timeCheck = DateTimeOffset.Now.AddMinutes(-forgotpwdexpirytime);
                var verificationCodes = await _repoFactory.GetRepo().FindAll<UserVerificationCodes>()
                                                    .Where(a => a.ShortVerificationCode == shortcode &&
                                                                a.UserId == appUser.Id &&
                                                                a.UserVerificationCodeTypeId == UserVerificationCodeTypeEnum.ForgotPassword.Value &&
                                                                a.CreatedDate > timeCheck)
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
                    string validateDatajsonPayload = JsonConvert.SerializeObject(validateData);
                    var checkPasswordHistroy = await _apiHelper.SendRequestAsync(validateDatajsonPayload, authapiUrl + "/api/AccountAPI/CheckPasswordHistroy", HttpMethod.Post);

                    if (!checkPasswordHistroy.IsSuccessStatusCode)
                    {
                        packet.AddError("Error", checkPasswordHistroy.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        var dataReset = new
                        {
                            Email = tenantId + "_" + model.Email,
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
            
            _logger.LogInformation("ResetPassword : End");
            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
