using System;
using ENTiger.ENCollect;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// Command that starts a new dynamic saga workflow.
    /// 'WorkflowInstanceId' is used for correlation; 
    /// 'DomainId' is also provided for direct domain usage in the saga.
    /// </summary>
    public class StartDynaWorkflowCommand : FlexCommandBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; } = string.Empty;
        public string DomainId { get; set; }
    }
}