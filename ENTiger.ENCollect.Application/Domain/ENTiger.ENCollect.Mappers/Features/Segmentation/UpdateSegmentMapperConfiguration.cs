using ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Input.UpdateSegment;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateSegmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateSegmentMapperConfiguration() : base()
        {
            CreateMap<UpdateSegmentDto, Segmentation>()
             .ForMember(vm => vm.SegmentAdvanceFilter, dm => dm.MapFrom(dModel => dModel.SegmentationFilterInputModel));

            CreateMap<EditSegmentationFilterDto, SegmentationAdvanceFilter>();
        }
    }
}