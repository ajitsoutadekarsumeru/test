using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUnsequencedAutoSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUnsequencedAutoSegmentsMapperConfiguration() : base()
        {
            CreateMap<Segmentation, GetUnsequencedAutoSegmentsDto>();
        }
    }
}