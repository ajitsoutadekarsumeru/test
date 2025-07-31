using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeviceDetailMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeviceDetailMapperConfiguration() : base()
        {
            CreateMap<DeviceDetailDto, DeviceDetail>();
            CreateMap<DeviceDetail, DeviceDetailDto>();
            CreateMap<DeviceDetailDtoWithId, DeviceDetail>();
            CreateMap<DeviceDetail, DeviceDetailDtoWithId>();
        }
    }
}