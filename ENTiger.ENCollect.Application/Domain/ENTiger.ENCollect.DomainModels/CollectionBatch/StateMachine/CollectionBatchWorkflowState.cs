using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class CollectionBatchWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual CollectionBatchWorkflowState CreateCollectionBatch() => this;

        public virtual CollectionBatchWorkflowState AcknowledgeCollectionBatch() => this;

        public virtual CollectionBatchWorkflowState AddCollectionBatchInPayInSlip() => this;

        public virtual CollectionBatchWorkflowState Dissolve() => this;

        public virtual CollectionBatchWorkflowState CreateCollectionBatchForPartner() => this;

        public virtual CollectionBatchWorkflowState AcknowledgeCollectionBatchForPartner() => this;
    }
}