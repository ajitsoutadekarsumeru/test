using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCityByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCityByIdMapperConfiguration() : base()
        {
            CreateMap<Cities, GetCityByIdDto>();
        }
    }
}