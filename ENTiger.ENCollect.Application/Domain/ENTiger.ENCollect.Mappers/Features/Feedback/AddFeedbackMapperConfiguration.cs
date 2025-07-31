using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddFeedbackMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddFeedbackMapperConfiguration() : base()
        {
            CreateMap<AddFeedbackDto, Feedback>();
        }
    }
}