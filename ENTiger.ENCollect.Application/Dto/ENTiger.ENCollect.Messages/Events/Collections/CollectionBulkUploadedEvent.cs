using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionBulkUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string CustomId { get; set; }
        public string? Id { get; set; }
        public string? ToEmailAddress { get; set; }

        public string? FileType { get; set; }
        public string? FileName { get; set; }
    }

    
}
