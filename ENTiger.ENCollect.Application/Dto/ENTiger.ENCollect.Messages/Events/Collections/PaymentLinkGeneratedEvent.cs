namespace ENTiger.ENCollect.CollectionsModule
{
    public class PaymentLinkGeneratedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? PaymentPartner { get; set; }

        //paymentTransaction
        public string? PaymentTransactionId { get; set; }

        //CollectionDtoWithId
        public string? CollectionId { get; set; }
    }
}