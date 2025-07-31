using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTrailIntensityDownloadDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTrailIntensityDownloadDetailsMapperConfiguration() : base()
        {
            CreateMap<TrailIntensityDownload, GetTrailIntensityDownloadDetailsDto>()
            .ForMember(vm => vm.TransactionId, dm => dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.Date, dm => dm.MapFrom(dModel => dModel.LastModifiedDate));
        }
    }
}