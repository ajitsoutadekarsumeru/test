using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TrailGapDownloadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TrailGapDownloadMapperConfiguration() : base()
        {
            CreateMap<TrailGapDownloadDto, TrailGapDownload>();
            CreateMap<TrailGapDownload, TrailGapDownloadDto>();
            CreateMap<TrailGapDownloadDtoWithId, TrailGapDownload>();
            CreateMap<TrailGapDownload, TrailGapDownloadDtoWithId>();
        }
    }
}