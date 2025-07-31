using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationAdvanceFilterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SegmentationAdvanceFilterMapperConfiguration() : base()
        {
            CreateMap<SegmentationAdvanceFilterDto, SegmentationAdvanceFilter>();
            CreateMap<SegmentationAdvanceFilter, SegmentationAdvanceFilterDto>();
            CreateMap<SegmentationAdvanceFilterDtoWithId, SegmentationAdvanceFilter>();
            CreateMap<SegmentationAdvanceFilter, SegmentationAdvanceFilterDtoWithId>();
        }
    }
}