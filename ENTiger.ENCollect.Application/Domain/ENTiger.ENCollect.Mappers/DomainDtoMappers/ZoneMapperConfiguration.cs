using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ZoneMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ZoneMapperConfiguration() : base()
        {
            CreateMap<ZoneDto, Zone>();
            CreateMap<Zone, ZoneDto>();
            CreateMap<ZoneDtoWithId, Zone>();
            CreateMap<Zone, ZoneDtoWithId>();
        }
    }
}