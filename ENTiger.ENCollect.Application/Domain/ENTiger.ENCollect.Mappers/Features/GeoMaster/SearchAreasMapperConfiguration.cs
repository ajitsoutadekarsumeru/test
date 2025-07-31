using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAreasMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAreasMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, SearchAreasDto>();
        }
    }
}