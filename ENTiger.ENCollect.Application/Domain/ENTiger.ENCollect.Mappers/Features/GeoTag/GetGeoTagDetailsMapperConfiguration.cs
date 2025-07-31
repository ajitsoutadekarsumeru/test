using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetGeoTagDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetGeoTagDetailsMapperConfiguration() : base()
        {
            CreateMap<GeoTagDetails, GetGeoTagDetailsDto>();
        }
    }
}