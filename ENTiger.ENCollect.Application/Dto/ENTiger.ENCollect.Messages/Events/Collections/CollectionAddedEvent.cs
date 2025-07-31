namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionAddedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
        public string CollectionId { get; set; }
        public string ReservationId { get; set; }
        public string CollectorId { get; set; }
    }
}