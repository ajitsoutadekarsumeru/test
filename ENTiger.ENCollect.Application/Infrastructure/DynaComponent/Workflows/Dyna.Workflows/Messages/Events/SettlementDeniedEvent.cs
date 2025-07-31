using System;
using ENTiger.ENCollect;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// Event after a deny action is processed in the domain.
    /// The saga will see this and end the workflow immediately.
    /// </summary>
    public class SettlementDeniedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; }
        public string DomainId { get; set; }
    }
}