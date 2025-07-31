using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RegionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RegionsMapperConfiguration() : base()
        {
            CreateMap<RegionsDto, Regions>();
            CreateMap<Regions, RegionsDto>();
            CreateMap<RegionsDtoWithId, Regions>();
            CreateMap<Regions, RegionsDtoWithId>();
        }
    }
}