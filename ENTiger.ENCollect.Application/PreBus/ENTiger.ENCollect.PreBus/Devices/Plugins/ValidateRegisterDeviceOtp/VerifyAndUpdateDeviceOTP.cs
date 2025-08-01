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

namespace ENTiger.ENCollect.DevicesModule.ValidateRegisterDeviceOtpDevicesPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VerifyAndUpdateDeviceOTP : FlexiBusinessRuleBase, IFlexiBusinessRule<ValidateRegisterDeviceOtpDataPacket>
    {
        public override string Id { get; set; } = "3a131f3d2e83c7f58ae54004c3321985";
        public override string FriendlyName { get; set; } = "VerifyAndUpdateDeviceOTP";

        protected readonly ILogger<VerifyAndUpdateDeviceOTP> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly OtpSettings _otpSettings;
        public VerifyAndUpdateDeviceOTP(ILogger<VerifyAndUpdateDeviceOTP> logger, IRepoFactory repoFactory, IOptions<OtpSettings> otpSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _otpSettings = otpSettings.Value;
        }

        public virtual async Task Validate(ValidateRegisterDeviceOtpDataPacket packet)
        {
            int otpExpiryTime = _otpSettings.Expiry.LoginOtpInMins;
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            DeviceDetail oDevice = await _repoFactory.GetRepo().FindAll<DeviceDetail>()
                            .Where(x => x.IMEI == packet.Dto.IMEI && x.OTP == packet.Dto.OTP)
                            .OrderByDescending(i => i.CreatedDate)
                            .FirstOrDefaultAsync();

            if (oDevice != null)
            {
                if (DateTimeOffset.Now.Subtract((oDevice.OTPTimeStamp)).TotalMinutes > otpExpiryTime)
                {
                    packet.AddError("Error", "OTP Expired");
                }
                else
                {
                    oDevice.IsVerified = true;
                    oDevice.SetLastModifiedDate(DateTimeOffset.Now);
                    oDevice.VerifiedDate = DateTime.Now;

                    _repoFactory.GetRepo().InsertOrUpdate(oDevice);
                    int records = await _repoFactory.GetRepo().SaveAsync();

                    //Disable other users registered on this IMEI number 
                    var devices = await _repoFactory.GetRepo().FindAll<DeviceDetail>()
                                            .Where(x => x.IMEI == packet.Dto.IMEI && x.OTP != packet.Dto.OTP)
                                            .ToListAsync();

                    if (devices != null && devices.Count() > 0)
                    {
                        foreach (var obj in devices)
                        {
                            obj.IsVerified = false;
                            _repoFactory.GetRepo().InsertOrUpdate(obj);
                        }
                        int result = await _repoFactory.GetRepo().SaveAsync();
                    }
                }
            }
            else
            {
                packet.AddError("Error", "Invalid OTP. Please try again.");
            }
        }
    }
}
