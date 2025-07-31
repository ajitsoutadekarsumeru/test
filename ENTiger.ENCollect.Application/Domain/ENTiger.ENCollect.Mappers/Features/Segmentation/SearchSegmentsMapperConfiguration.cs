using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchSegmentsMapperConfiguration() : base()
        {
            CreateMap<Segmentation, SearchSegmentsDto>()
           .ForMember(vm => vm.SegmentationName, dm => dm.MapFrom(dModel => dModel.Name))
           .ForMember(vm => vm.LastExecutionDate, dm => dm.MapFrom(dModel => dModel.LastModifiedDate))
           .ForMember(vm => vm.IsDisabled, dm => dm.MapFrom(dModel => dModel.IsDisabled))

           ;
        }
    }
}