using System;
using ENTiger.ENCollect;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
  
    public class ActorIndentifiedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; }
        public string DomainId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}