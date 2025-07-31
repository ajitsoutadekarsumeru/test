using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GeoMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GeoMasterMapperConfiguration() : base()
        {
            CreateMap<GeoMasterDto, GeoMaster>();
            CreateMap<GeoMaster, GeoMasterDto>();
            CreateMap<GeoMasterDtoWithId, GeoMaster>();
            CreateMap<GeoMaster, GeoMasterDtoWithId>();
        }
    }
}