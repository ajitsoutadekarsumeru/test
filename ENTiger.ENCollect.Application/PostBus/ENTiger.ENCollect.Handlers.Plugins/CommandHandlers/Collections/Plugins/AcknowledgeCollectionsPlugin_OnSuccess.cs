using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AcknowledgeCollectionsPlugin : FlexiPluginBase, IFlexiPlugin<AcknowledgeCollectionsPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CollectionAcknowledgedEvent @event = new CollectionAcknowledgedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CollectionIds = collectionIds,
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}