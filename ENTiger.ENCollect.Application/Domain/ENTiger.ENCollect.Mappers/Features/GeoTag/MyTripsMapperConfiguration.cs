using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MyTripsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MyTripsMapperConfiguration() : base()
        {
            CreateMap<GeoTagDetails, MyTripsDto>();
        }
    }
}