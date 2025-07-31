using DocumentFormat.OpenXml.Spreadsheet;
using Elastic.Clients.Elasticsearch.MachineLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.ApplicationUsersModule.VerifyForgotPasswordOTPApplicationUsersPlugins
{
    public partial class VerifyOTP : FlexiBusinessRuleBase, IFlexiBusinessRule<VerifyForgotPasswordOTPDataPacket>
    {
        public override string Id { get; set; } = "3a134c6777a99746acfef33499ae35de";
        public override string FriendlyName { get; set; } = "VerifyOTP";

        protected readonly ILogger<VerifyOTP> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly AuthSettings _authSettings;
        private readonly OtpSettings _otpSettings;
        private readonly IApiHelper _apiHelper;

        public VerifyOTP(ILogger<VerifyOTP> logger, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings
                , IOptions<OtpSettings> otpSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _otpSettings = otpSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Validate(VerifyForgotPasswordOTPDataPacket packet)
        {
            string authUrl = _authSettings.AuthUrl;
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();

            string tenantId = _flexAppContext.TenantId;
            //If any validation fails, uncomment and use the below line of code to add error to the packet
            ApplicationUser? appUser;
            UserVerificationCodes? verificationCodes;
            string shortcode = packet.Dto.Code;
            string authapiUrl = authUrl;

            string verifycode = string.Empty;

            var data = new
            {
                email = tenantId + "_" + packet.Dto.emailId,
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
                appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();

                DateTimeOffset timeCheck = new DateTimeOffset();
                Int32 forgotpwdexpirytime = _otpSettings.Expiry.ForgotPasswordOtpInMins;

                string? userVerificationCodeTypeId = await _repoFactory.GetRepo().FindAll<UserVerificationCodeTypes>()
                                                            .Where(i => string.Equals(i.Description, "forgotpassword"))
                                                            .Select(i => i.Id).FirstOrDefaultAsync();

                timeCheck = DateTimeOffset.Now.AddMinutes(-forgotpwdexpirytime);
                verificationCodes = await _repoFactory.GetRepo().FindAll<UserVerificationCodes>()
                                            .Where(a => a.ShortVerificationCode == shortcode &&
                                                        a.UserId == appUser.Id &&
                                                        a.UserVerificationCodeTypeId == userVerificationCodeTypeId &&
                                                        a.CreatedDate > timeCheck)
                                            .OrderByDescending(v => v.Id)
                                            .ThenByDescending(v => v.CreatedDate)
                                            .FirstOrDefaultAsync();

                if (verificationCodes != null)
                {
                    packet.OutputDto = "Code is verified succesfuly";
                }
                else
                {
                    packet.AddError("Error", "Code does not match/expired");
                }
            }
        }
    }
}
