using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionAcknowledged : CollectionWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionAcknowledged()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionWorkflowState InitiateCollection() => this;

        public override CollectionWorkflowState ReceiveCollection() => this;

        public override CollectionWorkflowState AcknowledgeCollection() => this;

        public override CollectionWorkflowState AddCollectionInBatch()
        {
            return _flexHost.GetFlexStateInstance<AddedCollectionInBatch>();
        }

        public override CollectionWorkflowState MakeReadyForBatch() => this;

        public override CollectionWorkflowState RequestCancellation()
        {
            return _flexHost.GetFlexStateInstance<CancellationRequested>();
        }

        public override CollectionWorkflowState Cancel() => this;

        public override CollectionWorkflowState Reject() => this;

        public override CollectionWorkflowState MarkCollectionAsFail() => this;

        public override CollectionWorkflowState MarkAsBouncedInCBS() => this;

        public override CollectionWorkflowState MarkAsUpdatedInCBS() => this;

        public override CollectionWorkflowState MarkAsErrorInCBS() => this;

        public override CollectionWorkflowState MarkAsPendingPostingInCBS() => this;

        public override CollectionWorkflowState MarkCollectionAsSuccess() => this;

        public override CollectionWorkflowState AddCollectionInPartnerBatch()
        {
            return _flexHost.GetFlexStateInstance<AddedCollectionInPartnerBatch>();
        }

        #endregion State
    }
}