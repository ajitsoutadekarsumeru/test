using System;
using ENTiger.ENCollect;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// Published if the saga remains active after receiving 
    /// an initiate action. If the workflow immediately ends (no steps),
    /// no event is published.
    /// 
    /// Contains only the WorkflowInstanceId. 
    /// If domain wants more data, override OnWorkflowInitiated in final saga.
    /// </summary>
    public class DynaReNegotiateWorkflowInitiatedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; }
        public string DomainId { get; set; }
        
    }
}