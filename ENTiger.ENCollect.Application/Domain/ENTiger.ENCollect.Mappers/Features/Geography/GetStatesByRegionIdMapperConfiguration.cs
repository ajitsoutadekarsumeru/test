using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetStatesByRegionIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetStatesByRegionIdMapperConfiguration() : base()
        {
            CreateMap<State, GetStatesByRegionIdDto>();
        }
    }
}