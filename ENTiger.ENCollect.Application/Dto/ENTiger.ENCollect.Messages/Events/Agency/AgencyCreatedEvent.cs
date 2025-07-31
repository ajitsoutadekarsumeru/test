namespace ENTiger.ENCollect.AgencyModule
{
    public class AgencyCreatedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}