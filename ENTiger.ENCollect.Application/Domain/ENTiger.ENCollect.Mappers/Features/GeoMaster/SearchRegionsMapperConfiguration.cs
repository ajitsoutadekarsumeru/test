using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchRegionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchRegionsMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, SearchRegionsDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.Region));
        }
    }
}