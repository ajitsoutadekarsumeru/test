using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class EnableSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public EnableSegmentsMapperConfiguration() : base()
        {
            CreateMap<EnableSegmentsDto, Segmentation>();
        }
    }
}