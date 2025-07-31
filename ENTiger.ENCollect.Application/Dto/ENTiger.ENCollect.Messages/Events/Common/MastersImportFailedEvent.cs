namespace ENTiger.ENCollect.CommonModule
{
    public class MastersImportFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }

        public string ErrorReason { get; set; }
    }
}