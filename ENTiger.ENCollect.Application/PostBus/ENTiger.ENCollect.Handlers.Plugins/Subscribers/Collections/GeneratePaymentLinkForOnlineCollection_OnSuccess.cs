namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class GeneratePaymentLinkForOnlineCollection
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            PaymentLinkGeneratedEvent @event = new PaymentLinkGeneratedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CollectionId = collection.Id,
                PaymentPartner = paymentPartner,
                PaymentTransactionId = paymentTransaction.Id

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}