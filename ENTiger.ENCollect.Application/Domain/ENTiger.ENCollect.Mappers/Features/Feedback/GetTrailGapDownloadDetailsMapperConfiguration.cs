using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTrailGapDownloadDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTrailGapDownloadDetailsMapperConfiguration() : base()
        {
            CreateMap<TrailGapDownload, GetTrailGapDownloadDetailsDto>()
            .ForMember(vm => vm.Date, dm => dm.MapFrom(dModel => dModel.LastModifiedDate))
            .ForMember(vm => vm.TransactionId, dm => dm.MapFrom(dModel => dModel.CustomId));
        }
    }
}