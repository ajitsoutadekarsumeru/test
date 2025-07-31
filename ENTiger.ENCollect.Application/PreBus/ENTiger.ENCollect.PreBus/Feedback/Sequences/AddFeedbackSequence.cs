using ENTiger.ENCollect.FeedbackModule.AddFeedbackFeedbackPlugins;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public class AddFeedbackSequence : FlexiBusinessRuleSequenceBase<AddFeedbackDataPacket>
    {
        public AddFeedbackSequence()
        {
            this.Add<CheckUserFeedbackLimit>(); this.Add<ValidateFeedback>(); 
        }
    }
}
