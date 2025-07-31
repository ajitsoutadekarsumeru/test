namespace ENTiger.ENCollect.CommonModule
{
    public class MastersImportProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public int processedrecordscount;
        public int recordsinserted;
        public int recordsupdated;
        public int nooferrorrecords;
        public int totalrecords;
    }
}