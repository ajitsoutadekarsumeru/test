using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.PublicModule
{
    public class CollectionImportedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
    }

    
}
