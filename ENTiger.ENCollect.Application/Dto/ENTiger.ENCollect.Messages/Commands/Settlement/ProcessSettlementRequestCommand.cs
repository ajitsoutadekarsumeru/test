using ENTiger.ENCollect.SettlementModule;

namespace ENTiger.ENCollect
{
    public class ProcessSettlementRequestCommand : FlexCommandBridge<FlexAppContextBridge>
    {
        public string WorkflowInstanceId { get; set; } = string.Empty;
        public string DomainId { get; set; }
        public object Dto { get; set; }

    }
}