﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule.VerifyLoginOTPApplicationUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VerifyOTP : FlexiBusinessRuleBase, IFlexiBusinessRule<VerifyLoginOTPDataPacket>
    {
        public override string Id { get; set; } = "3a134c68a39da1f8882e7a7896fdcfd3";
        public override string FriendlyName { get; set; } = "VerifyOTP";

        protected readonly ILogger<VerifyOTP> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly OtpSettings _otpSettings;
        public VerifyOTP(ILogger<VerifyOTP> logger, IRepoFactory repoFactory, IOptions<OtpSettings> otpSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _otpSettings = otpSettings.Value;
        }

        public virtual async Task Validate(VerifyLoginOTPDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            var appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.PrimaryEMail == packet.Dto.emailId && x.IsDeleted == false).FirstOrDefaultAsync();
            if (appUser == null)
            {
                _logger.LogWarning("User not found with email: {Email}", packet.Dto.emailId);
                packet.AddError("Error", "User not found.");
                return;
            }

            var expiryTime = _otpSettings.Expiry.LoginOtpInMins;
            var timeCheck = DateTime.Now.AddMinutes(-expiryTime);

            var verificationCode = await _repoFactory.GetRepo().FindAll<UserVerificationCodes>()
                                            .Where(a => !a.IsDeleted &&
                                                        a.UserId == appUser.Id &&
                                                        a.UserVerificationCodeTypeId == UserVerificationCodeTypeEnum.LoginOtp.Value &&
                                                        a.ShortVerificationCode == packet.Dto.Code)
                                            .OrderByDescending(v => v.CreatedDate)
                                            .FirstOrDefaultAsync();

            LoginDetailsHistory loginDetailsHistory = new LoginDetailsHistory();
            loginDetailsHistory.UserId = appUser.Id;

            if (verificationCode == null)
            {
                _logger.LogWarning("Invalid OTP for user: {UserId}", appUser.Id);
                packet.AddError("Error", "Please Enter Valid OTP");
                loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                loginDetailsHistory.Remarks = "Invalid OTP";
            }
            else 
            {
                if (timeCheck <= verificationCode.CreatedDate)
                {
                    verificationCode.SetIsDeleted(true);
                    verificationCode.SetModified();
                    _repoFactory.GetRepo().InsertOrUpdate(verificationCode);
                }
                else
                {
                    packet.AddError("Error", "OTP expired");
                    loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                    loginDetailsHistory.Remarks = "OTP expired";
                }
            }            
            if (packet.HasError)
            {
                loginDetailsHistory.SetAddedOrModified();
                _repoFactory.GetRepo().InsertOrUpdate(loginDetailsHistory);
            }
            int records = await _repoFactory.GetRepo().SaveAsync();            
        }
    }
}
