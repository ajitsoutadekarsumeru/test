using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationAdvanceFilterMastersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SegmentationAdvanceFilterMastersMapperConfiguration() : base()
        {
            CreateMap<SegmentationAdvanceFilterMastersDto, SegmentationAdvanceFilterMasters>();
            CreateMap<SegmentationAdvanceFilterMasters, SegmentationAdvanceFilterMastersDto>();
            CreateMap<SegmentationAdvanceFilterMastersDtoWithId, SegmentationAdvanceFilterMasters>();
            CreateMap<SegmentationAdvanceFilterMasters, SegmentationAdvanceFilterMastersDtoWithId>();
        }
    }
}