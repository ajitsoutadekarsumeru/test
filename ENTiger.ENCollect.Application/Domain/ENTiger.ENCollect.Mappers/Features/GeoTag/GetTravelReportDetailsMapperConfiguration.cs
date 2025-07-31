using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTravelReportDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTravelReportDetailsMapperConfiguration() : base()
        {
            CreateMap<GeoTagDetails, GetTravelReportDetailsDto>();
        }
    }
}