namespace ENTiger.ENCollect.PayInSlipsModule
{
    public class PayInSlipCreatedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}