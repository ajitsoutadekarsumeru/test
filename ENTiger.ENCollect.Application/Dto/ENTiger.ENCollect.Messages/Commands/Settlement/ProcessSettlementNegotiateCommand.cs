using ENTiger.ENCollect.SettlementModule;

namespace ENTiger.ENCollect
{
    public class ProcessSettlementNegotiateCommand : FlexCommandBridge<FlexAppContextBridge>, IDynaWorkflowDomainCommand
    {
        public string DomainId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string WorkflowInstanceId { get; set; } = string.Empty;
        public string StepName { get; set; }
        public string StepType { get; set; }
        public object Dto { get; set; }
    }
}