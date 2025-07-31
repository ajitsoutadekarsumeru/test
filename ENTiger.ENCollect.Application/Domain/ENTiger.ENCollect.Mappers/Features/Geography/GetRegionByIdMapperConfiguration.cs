using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetRegionByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetRegionByIdMapperConfiguration() : base()
        {
            CreateMap<Regions, GetRegionByIdDto>();
        }
    }
}