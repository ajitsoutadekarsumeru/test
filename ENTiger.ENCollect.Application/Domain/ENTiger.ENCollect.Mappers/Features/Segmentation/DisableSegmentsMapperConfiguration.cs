using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DisableSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DisableSegmentsMapperConfiguration() : base()
        {
            CreateMap<DisableSegmentsDto, Segmentation>();
        }
    }
}