using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionBulkUploadProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }

    
}
