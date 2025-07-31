using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class UpdateSegmentPlugin : FlexiPluginBase, IFlexiPlugin<UpdateSegmentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            SegmentUpdatedEvent @event = new SegmentUpdatedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}