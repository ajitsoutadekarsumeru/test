using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CountriesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CountriesMapperConfiguration() : base()
        {
            CreateMap<CountriesDto, Countries>();
            CreateMap<Countries, CountriesDto>();
            CreateMap<CountriesDtoWithId, Countries>();
            CreateMap<Countries, CountriesDtoWithId>();
        }
    }
}