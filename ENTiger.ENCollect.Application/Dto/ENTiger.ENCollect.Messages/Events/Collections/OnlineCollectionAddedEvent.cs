namespace ENTiger.ENCollect.CollectionsModule
{
    public class OnlineCollectionAddedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? CollectionId { get; set; }

        public string? CollectorId { get; set; }

        public string? LoanAccountId { get; set; }

        public string? PaymentPartner { get; set; }
    }
}