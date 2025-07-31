using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionBulkUploadFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }

    
}
