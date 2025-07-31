using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddPhysicalCollectionPlugin : FlexiPluginBase, IFlexiPlugin<AddPhysicalCollectionPostBusDataPacket>
    {
        protected const string CONDITION_ONONLINEPAYMENT = "OnOnlinePayment";

        protected virtual async Task OnOnlinePayment(IFlexServiceBusContextBridge serviceBusContext)
        {
            OnlineCollectionAddedEvent @event = new OnlineCollectionAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CollectorId = _model.CollectorId
            };

            await serviceBusContext.Publish(@event);
        }
    }
}