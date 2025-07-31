using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCitiesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCitiesMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, SearchCitiesDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.CITY));
        }
    }
}