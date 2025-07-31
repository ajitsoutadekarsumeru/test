using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCitiesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCitiesMapperConfiguration() : base()
        {
            CreateMap<Cities, GetCitieDto>();
        }
    }
}