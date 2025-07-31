using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AddedCollectionInPartnerBatch : CollectionWorkflowState
    {
        private IFlexHost _flexHost;

        public AddedCollectionInPartnerBatch()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionWorkflowState InitiateCollection() => this;

        public override CollectionWorkflowState ReceiveCollection() => this;

        public override CollectionWorkflowState AcknowledgeCollection() => this;

        public override CollectionWorkflowState AddCollectionInBatch() => this;

        public override CollectionWorkflowState MakeReadyForBatch()
        {
            return _flexHost.GetFlexStateInstance<ReadyForBatch>();
        }

        public override CollectionWorkflowState RequestCancellation() => this;

        public override CollectionWorkflowState Cancel() => this;

        public override CollectionWorkflowState Reject() => this;

        public override CollectionWorkflowState MarkCollectionAsFail() => this;

        public override CollectionWorkflowState MarkAsBouncedInCBS() => this;

        public override CollectionWorkflowState MarkAsUpdatedInCBS() => this;

        public override CollectionWorkflowState MarkAsErrorInCBS() => this;

        public override CollectionWorkflowState MarkAsPendingPostingInCBS() => this;

        public override CollectionWorkflowState MarkCollectionAsSuccess() => this;

        public override CollectionWorkflowState AddCollectionInPartnerBatch() => this;

        #endregion State
    }
}