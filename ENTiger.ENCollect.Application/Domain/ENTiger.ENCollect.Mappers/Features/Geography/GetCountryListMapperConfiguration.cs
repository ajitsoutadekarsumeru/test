using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCountryListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCountryListMapperConfiguration() : base()
        {
            CreateMap<Countries, GetCountryListDto>();
        }
    }
}