using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCountryByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCountryByIdMapperConfiguration() : base()
        {
            CreateMap<Countries, GetCountryByIdDto>();
        }
    }
}