using System;
using ENTiger.ENCollect;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// Event published by the domain after 
    /// a recommendation action is processed.
    /// The saga listens for it to continue the workflow.
    /// </summary>
    public class SettlementRecommendedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; }
        public string DomainId { get; set; }
    }
}