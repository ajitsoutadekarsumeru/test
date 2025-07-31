using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddGeoTagMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddGeoTagMapperConfiguration() : base()
        {
            CreateMap<AddGeoTagDto, GeoTagDetails>();
        }
    }
}