using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GeoTagDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GeoTagDetailsMapperConfiguration() : base()
        {
            CreateMap<GeoTagDetailsDto, GeoTagDetails>();
            CreateMap<GeoTagDetails, GeoTagDetailsDto>();
            CreateMap<GeoTagDetailsDtoWithId, GeoTagDetails>();
            CreateMap<GeoTagDetails, GeoTagDetailsDtoWithId>();
        }
    }
}