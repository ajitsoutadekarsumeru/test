using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class AcknowledgeCollectionBatchPlugin : FlexiPluginBase, IFlexiPlugin<AcknowledgeCollectionBatchPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CollectionBatchAcknowledgedEvent @event = new CollectionBatchAcknowledgedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}