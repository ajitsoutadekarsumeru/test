using System;
using ENTiger.ENCollect;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// Event after an approve action is done.
    /// The saga sees this => moves to next step or completes.
    /// </summary>
    public class SettlementApprovedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; }
        public string DomainId { get; set; }
    }
}