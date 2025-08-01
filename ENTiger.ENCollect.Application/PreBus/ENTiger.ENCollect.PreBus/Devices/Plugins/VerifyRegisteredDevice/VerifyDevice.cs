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
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.DevicesModule.VerifyRegisteredDeviceDevicesPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VerifyDevice : FlexiBusinessRuleBase, IFlexiBusinessRule<VerifyRegisteredDeviceDataPacket>
    {
        public override string Id { get; set; } = "3a131f3de6ba9493e8bfc1faf0c2bc0b";
        public override string FriendlyName { get; set; } = "VerifyDevice";

        protected readonly ILogger<VerifyDevice> _logger;
        protected readonly IRepoFactory _repoFactory;

        public VerifyDevice(ILogger<VerifyDevice> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(VerifyRegisteredDeviceDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            if (!packet.HasError)
            {
                var device = await _repoFactory.GetRepo().FindAll<DeviceDetail>()
                                        .Where(a => a.Email == packet.Dto.Email &&
                                                    a.IMEI == packet.Dto.IMEI &&
                                                    a.IsVerified == true)
                                        .ToListAsync();

                if (device == null || string.IsNullOrEmpty(packet.Dto.IMEI))
                {
                    packet.AddError("Error", "Device not registered");
                }
            }
        }
    }
}
