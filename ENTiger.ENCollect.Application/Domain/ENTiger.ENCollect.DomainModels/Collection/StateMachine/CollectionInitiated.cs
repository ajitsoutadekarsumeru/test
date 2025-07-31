using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionInitiated : CollectionWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionInitiated()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionWorkflowState InitiateCollection() => this;

        public override CollectionWorkflowState ReceiveCollection()
        {
            return _flexHost.GetFlexStateInstance<ReceivedByCollector>();
        }

        public override CollectionWorkflowState AcknowledgeCollection()
        {
            return _flexHost.GetFlexStateInstance<CollectionAcknowledged>();
        }

        public override CollectionWorkflowState AddCollectionInBatch() => this;

        public override CollectionWorkflowState MakeReadyForBatch() => this;

        public override CollectionWorkflowState RequestCancellation() => this;

        public override CollectionWorkflowState Cancel() => this;

        public override CollectionWorkflowState Reject() => this;

        public override CollectionWorkflowState MarkCollectionAsFail()
        {
            return _flexHost.GetFlexStateInstance<CollectionFailed>();
        }

        public override CollectionWorkflowState MarkAsBouncedInCBS() => this;

        public override CollectionWorkflowState MarkAsUpdatedInCBS() => this;

        public override CollectionWorkflowState MarkAsErrorInCBS() => this;

        public override CollectionWorkflowState MarkAsPendingPostingInCBS() => this;

        public override CollectionWorkflowState MarkCollectionAsSuccess()
        {
            return _flexHost.GetFlexStateInstance<CollectionSuccess>();
        }

        public override CollectionWorkflowState AddCollectionInPartnerBatch() => this;

        #endregion State
    }
}