using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackMapperConfiguration : FlexMapperProfile
    {
        public FeedbackMapperConfiguration() : base()
        {
            CreateMap<FeedbackDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDtoWithId, Feedback>();
            CreateMap<Feedback, FeedbackDtoWithId>();

            CreateMap<Feedback, AddFeedbackDto>();
        }
    }
}