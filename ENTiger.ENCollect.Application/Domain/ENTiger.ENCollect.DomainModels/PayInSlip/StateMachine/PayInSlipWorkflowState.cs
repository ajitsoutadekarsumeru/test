using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class PayInSlipWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual PayInSlipWorkflowState CreatePayInSlip() => this;

        public virtual PayInSlipWorkflowState AcknowledgePayInSlip() => this;
    }
}