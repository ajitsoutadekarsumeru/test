using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AreaMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AreaMapperConfiguration() : base()
        {
            CreateMap<AreaDto, Area>();
            CreateMap<Area, AreaDto>();
            CreateMap<AreaDtoWithId, Area>();
            CreateMap<Area, AreaDtoWithId>();
        }
    }
}