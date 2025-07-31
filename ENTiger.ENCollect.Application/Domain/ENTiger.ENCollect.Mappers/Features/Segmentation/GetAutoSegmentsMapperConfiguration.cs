using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAutoSegmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAutoSegmentsMapperConfiguration() : base()
        {
            CreateMap<Segmentation, GetAutoSegmentsDto>();
        }
    }
}