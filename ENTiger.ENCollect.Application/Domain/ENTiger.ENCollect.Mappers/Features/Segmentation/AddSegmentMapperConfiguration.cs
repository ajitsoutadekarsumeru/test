using ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Input.AddSegment;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddSegmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddSegmentMapperConfiguration() : base()
        {
            CreateMap<AddSegmentDto, Segmentation>()
            .ForMember(vm => vm.SegmentAdvanceFilter, dm => dm.MapFrom(dModel => dModel.SegmentationFilterInputModel));

            CreateMap<AddSegmentationFilterDto, SegmentationAdvanceFilter>();
        }
    }
}