using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RegisterDeviceMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RegisterDeviceMapperConfiguration() : base()
        {
            CreateMap<RegisterDeviceDto, DeviceDetail>();
        }
    }
}