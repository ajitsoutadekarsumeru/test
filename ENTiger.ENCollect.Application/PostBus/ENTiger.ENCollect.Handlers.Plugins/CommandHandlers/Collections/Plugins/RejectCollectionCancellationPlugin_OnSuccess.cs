using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class RejectCollectionCancellationPlugin : FlexiPluginBase, IFlexiPlugin<RejectCollectionCancellationPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            foreach (Collection collection in collections)
            {
                CollectionCancellationRejected @event = new CollectionCancellationRejected
                {
                    AppContext = _flexAppContext,  //do not remove this line
                    Id = collection.Id,
                    //Add your properties here
                };

                await serviceBusContext.Publish(@event);
            }
        }
    }
}