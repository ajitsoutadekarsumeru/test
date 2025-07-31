using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyRegisteredDeviceMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public VerifyRegisteredDeviceMapperConfiguration() : base()
        {
            CreateMap<VerifyRegisteredDeviceDto, DeviceDetail>();
        }
    }
}