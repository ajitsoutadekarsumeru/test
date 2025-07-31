using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class AccountsIdentifiedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string RunId { get; set; }
        public string TriggerId { get; set; }
        public string TriggerTypeId { get; set; }
        //actorIds
        public IReadOnlyList<string> ActorIds { get; set; } = Array.Empty<string>();
    }

    
}
