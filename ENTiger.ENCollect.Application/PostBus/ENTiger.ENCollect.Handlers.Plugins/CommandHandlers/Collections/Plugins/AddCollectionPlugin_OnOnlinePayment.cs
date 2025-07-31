using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddCollectionPlugin : FlexiPluginBase, IFlexiPlugin<AddCollectionPostBusDataPacket>
    {
        protected const string CONDITION_ONONLINEPAYMENT = "OnOnlinePayment";

        protected virtual async Task OnOnlinePayment(IFlexServiceBusContextBridge serviceBusContext)
        {
            OnlineCollectionAddedEvent @event = new OnlineCollectionAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}