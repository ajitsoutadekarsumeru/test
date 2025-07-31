using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeleteSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeleteSegmentsMapperConfiguration() : base()
        {
            CreateMap<DeleteSegmentsDto, Segmentation>();
        }
    }
}