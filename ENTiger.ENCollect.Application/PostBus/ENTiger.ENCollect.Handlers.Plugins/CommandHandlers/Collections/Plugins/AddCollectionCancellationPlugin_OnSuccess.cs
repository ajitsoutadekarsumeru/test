using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddCollectionCancellationPlugin : FlexiPluginBase, IFlexiPlugin<AddCollectionCancellationPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            foreach (Collection collection in collections)
            {
                CollectionCancellationAdded @event = new CollectionCancellationAdded
                {
                    AppContext = _flexAppContext,  //do not remove this line
                    Id = collection.Id,
                    MobileNo = collection.MobileNo
                    //Add your properties here
                };

                await serviceBusContext.Publish(@event);
            }
        }
    }
}