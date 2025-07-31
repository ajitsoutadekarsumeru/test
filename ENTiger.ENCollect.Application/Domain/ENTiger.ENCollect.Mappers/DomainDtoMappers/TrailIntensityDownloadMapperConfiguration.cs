using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TrailIntensityDownloadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TrailIntensityDownloadMapperConfiguration() : base()
        {
            CreateMap<TrailIntensityDownloadDto, TrailIntensityDownload>();
            CreateMap<TrailIntensityDownload, TrailIntensityDownloadDto>();
            CreateMap<TrailIntensityDownloadDtoWithId, TrailIntensityDownload>();
            CreateMap<TrailIntensityDownload, TrailIntensityDownloadDtoWithId>();
        }
    }
}