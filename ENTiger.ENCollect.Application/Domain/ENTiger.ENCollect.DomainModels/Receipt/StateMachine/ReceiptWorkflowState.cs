using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class ReceiptWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual ReceiptWorkflowState AllocateToCollector() => this;

        public virtual ReceiptWorkflowState CollectCollection() => this;
    }
}