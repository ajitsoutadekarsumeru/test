using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class AddFeedbackPlugin : FlexiPluginBase, IFlexiPlugin<AddFeedbackPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            FeedbackAddedEvent @event = new FeedbackAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
            };
            await serviceBusContext.Publish(@event);
        }
    }
}