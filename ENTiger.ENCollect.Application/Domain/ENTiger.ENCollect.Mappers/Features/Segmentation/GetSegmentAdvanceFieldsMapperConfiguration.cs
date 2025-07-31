using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentAdvanceFieldsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSegmentAdvanceFieldsMapperConfiguration() : base()
        {
            CreateMap<SegmentationAdvanceFilterMasters, GetSegmentAdvanceFieldsDto>();
        }
    }
}