using ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Output.GetSegmentById;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSegmentByIdMapperConfiguration() : base()
        {
            CreateMap<Segmentation, GetSegmentByIdDto>()
             .ForMember(vm => vm.SegmentationFilterInputModel, dm => dm.MapFrom(dModel => dModel.SegmentAdvanceFilter));

            ;

            CreateMap<SegmentationAdvanceFilter, ViewSegmentationFilterDto>();
        }
    }
}