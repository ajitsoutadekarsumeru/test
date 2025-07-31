namespace ENTiger.ENCollect.CollectionsModule
{
    public class UpdatePaymentStatusEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string>? Ids { get; set; }
    }
}
