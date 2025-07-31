using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCountryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCountryMapperConfiguration() : base()
        {
            CreateMap<Countries, SearchCountryDto>();
        }
    }
}