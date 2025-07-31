using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class CollectionWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual CollectionWorkflowState InitiateCollection() => this;

        public virtual CollectionWorkflowState ReceiveCollection() => this;

        public virtual CollectionWorkflowState AcknowledgeCollection() => this;

        public virtual CollectionWorkflowState AddCollectionInBatch() => this;

        public virtual CollectionWorkflowState MakeReadyForBatch() => this;

        public virtual CollectionWorkflowState RequestCancellation() => this;

        public virtual CollectionWorkflowState Cancel() => this;

        public virtual CollectionWorkflowState Reject() => this;

        public virtual CollectionWorkflowState MarkCollectionAsFail() => this;

        public virtual CollectionWorkflowState MarkAsBouncedInCBS() => this;

        public virtual CollectionWorkflowState MarkAsUpdatedInCBS() => this;

        public virtual CollectionWorkflowState MarkAsErrorInCBS() => this;

        public virtual CollectionWorkflowState MarkAsPendingPostingInCBS() => this;

        public virtual CollectionWorkflowState MarkCollectionAsSuccess() => this;

        public virtual CollectionWorkflowState AddCollectionInPartnerBatch() => this;
    }
}