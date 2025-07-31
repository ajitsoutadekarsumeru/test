namespace ENTiger.ENCollect.AuditTrailModule
{
    public class AuditTrailRequestedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public AuditEventData Data { get; set; }
    }
}
