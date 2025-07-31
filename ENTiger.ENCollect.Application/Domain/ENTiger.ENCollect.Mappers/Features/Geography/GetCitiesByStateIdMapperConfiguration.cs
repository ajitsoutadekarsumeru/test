using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCitiesByStateIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCitiesByStateIdMapperConfiguration() : base()
        {
            CreateMap<Cities, GetCitiesByStateIdDto>();
        }
    }
}