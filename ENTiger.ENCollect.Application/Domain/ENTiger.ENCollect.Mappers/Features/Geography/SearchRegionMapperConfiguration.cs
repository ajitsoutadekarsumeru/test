using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchRegionMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchRegionMapperConfiguration() : base()
        {
            CreateMap<Regions, SearchRegionDto>();
        }
    }
}