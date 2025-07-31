using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddLoginGeoTagMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddLoginGeoTagMapperConfiguration() : base()
        {
            CreateMap<AddLoginGeoTagDto, GeoTagDetails>();
        }
    }
}