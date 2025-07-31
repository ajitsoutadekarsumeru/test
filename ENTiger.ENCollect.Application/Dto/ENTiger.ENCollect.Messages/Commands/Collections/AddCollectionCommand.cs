namespace ENTiger.ENCollect.CollectionsModule
{
    public class AddCollectionCommand : FlexCommandBridge<AddCollectionDto, FlexAppContextBridge>
    {
        public string ReceiptId { get; set; }
        public string? ReservationId { get; set; }
    }
}