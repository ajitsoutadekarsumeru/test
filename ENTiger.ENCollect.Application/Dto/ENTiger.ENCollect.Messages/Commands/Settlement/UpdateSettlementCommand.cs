
namespace ENTiger.ENCollect.SettlementModule
{
    public class UpdateSettlementCommand : FlexCommandBridge<FlexAppContextBridge>, IDynaWorkflowDomainCommand
    {
        public string DomainId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string WorkflowInstanceId { get; set; } = string.Empty;
        public string StepName { get; set; }
        public string StepType { get; set; }
        public object Dto { get; set; }
    }
}
