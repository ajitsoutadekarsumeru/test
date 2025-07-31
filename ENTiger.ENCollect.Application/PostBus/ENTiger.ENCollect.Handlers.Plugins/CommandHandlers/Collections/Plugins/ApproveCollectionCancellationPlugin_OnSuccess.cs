using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ApproveCollectionCancellationPlugin : FlexiPluginBase, IFlexiPlugin<ApproveCollectionCancellationPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            foreach (Collection collection in collections)
            {
                CollectionCancellationApproved @event = new CollectionCancellationApproved
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