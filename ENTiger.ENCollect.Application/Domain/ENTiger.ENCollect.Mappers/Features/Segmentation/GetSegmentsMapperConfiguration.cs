using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSegmentsMapperConfiguration() : base()
        {
            CreateMap<Segmentation, GetSegmentsDto>();
        }
    }
}