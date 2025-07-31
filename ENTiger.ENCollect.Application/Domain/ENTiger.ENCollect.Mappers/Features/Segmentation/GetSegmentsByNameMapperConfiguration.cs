using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentsByNameMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSegmentsByNameMapperConfiguration() : base()
        {
            CreateMap<Segmentation, GetSegmentsByNameDto>();
        }
    }
}