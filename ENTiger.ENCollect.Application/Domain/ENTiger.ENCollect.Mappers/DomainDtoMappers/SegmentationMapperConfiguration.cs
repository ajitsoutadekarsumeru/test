using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SegmentationMapperConfiguration() : base()
        {
            CreateMap<SegmentationDto, Segmentation>();
            CreateMap<Segmentation, SegmentationDto>();
            CreateMap<SegmentationDtoWithId, Segmentation>();
            CreateMap<Segmentation, SegmentationDtoWithId>();
        }
    }
}