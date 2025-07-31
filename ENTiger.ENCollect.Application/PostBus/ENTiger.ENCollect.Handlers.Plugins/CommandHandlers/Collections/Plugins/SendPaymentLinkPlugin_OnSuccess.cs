using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentLinkPlugin : FlexiPluginBase, IFlexiPlugin<SendPaymentLinkPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            OnlineCollectionAddedEvent @event = new OnlineCollectionAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CollectionId = _model.Id,
                PaymentPartner = paymentPartner,
                CollectorId = _model.CollectorId,
                LoanAccountId = _model.AccountId
            };
            await serviceBusContext.Publish(@event);
        }
    }
}