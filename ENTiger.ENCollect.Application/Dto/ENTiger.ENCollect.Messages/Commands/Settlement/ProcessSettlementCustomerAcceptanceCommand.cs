using ENTiger.ENCollect.SettlementModule;

namespace ENTiger.ENCollect
{
    public class ProcessSettlementCustomerAcceptanceCommand : FlexCommandBridge<FlexAppContextBridge>, IDynaWorkflowDomainCommand
    {
        public string DomainId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string WorkflowInstanceId { get; set; }
        public string StepName { get; set; }
        public string StepType { get; set; }
        public object Dto { get; set; }
    }
}