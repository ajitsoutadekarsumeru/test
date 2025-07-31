using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetRegionListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetRegionListMapperConfiguration() : base()
        {
            CreateMap<Regions, GetRegionListDto>();
        }
    }
}