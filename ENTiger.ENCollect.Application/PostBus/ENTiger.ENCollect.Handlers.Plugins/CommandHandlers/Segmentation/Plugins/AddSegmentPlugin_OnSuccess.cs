using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class AddSegmentPlugin : FlexiPluginBase, IFlexiPlugin<AddSegmentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            SegmentAddedEvent @event = new SegmentAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}