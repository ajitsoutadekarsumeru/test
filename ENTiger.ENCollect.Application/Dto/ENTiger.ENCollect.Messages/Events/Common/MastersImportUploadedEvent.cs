namespace ENTiger.ENCollect.CommonModule
{
    public class MastersImportUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}