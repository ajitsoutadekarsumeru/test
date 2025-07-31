using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetFeedbacksByAccountNoMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetFeedbacksByAccountNoMapperConfiguration() : base()
        {
            CreateMap<Feedback, GetFeedbacksByAccountNoDto>()
            .ForMember(cm => cm.ContactNumber, Dm => Dm.MapFrom(dModel => dModel.NewContactNo))
            .ForMember(cm => cm.NewContactNumber, Dm => Dm.MapFrom(dModel => dModel.NewContactNo))
            .ForMember(cm => cm.Email, Dm => Dm.MapFrom(dModel => dModel.NewEmailId ?? ""));
        }
    }
}