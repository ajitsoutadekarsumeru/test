using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCityMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCityMapperConfiguration() : base()
        {
            CreateMap<Cities, SearchCityDto>();
        }
    }
}