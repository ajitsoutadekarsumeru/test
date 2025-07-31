using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateSegmentsSequenceMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateSegmentsSequenceMapperConfiguration() : base()
        {
            CreateMap<UpdateSegmentsSequenceDto, Segmentation>();
        }
    }
}