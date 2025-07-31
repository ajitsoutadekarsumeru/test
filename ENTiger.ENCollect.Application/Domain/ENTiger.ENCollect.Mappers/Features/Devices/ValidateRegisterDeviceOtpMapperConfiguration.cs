using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateRegisterDeviceOtpMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ValidateRegisterDeviceOtpMapperConfiguration() : base()
        {
            CreateMap<ValidateRegisterDeviceOtpDto, DeviceDetail>();
        }
    }
}