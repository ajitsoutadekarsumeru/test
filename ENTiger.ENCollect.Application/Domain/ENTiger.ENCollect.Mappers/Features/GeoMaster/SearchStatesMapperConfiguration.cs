using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchStatesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchStatesMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, SearchStatesDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.State));
        }
    }
}