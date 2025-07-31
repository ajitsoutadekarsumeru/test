using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ReceivedByCollector : CollectionWorkflowState
    {
        private IFlexHost _flexHost;

        public ReceivedByCollector()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionWorkflowState InitiateCollection() => this;

        public override CollectionWorkflowState ReceiveCollection() => this;

        public override CollectionWorkflowState AcknowledgeCollection()
        {
            return _flexHost.GetFlexStateInstance<CollectionAcknowledged>();
        }

        public override CollectionWorkflowState AddCollectionInBatch()
        {
            return _flexHost.GetFlexStateInstance<AddedCollectionInBatch>();
        }

        public override CollectionWorkflowState MakeReadyForBatch() => this;

        public override CollectionWorkflowState RequestCancellation() => this;

        public override CollectionWorkflowState Cancel() => this;

        public override CollectionWorkflowState Reject() => this;

        public override CollectionWorkflowState MarkCollectionAsFail() => this;

        public override CollectionWorkflowState MarkAsBouncedInCBS()
        {
            return _flexHost.GetFlexStateInstance<BouncedInCBS>();
        }

        public override CollectionWorkflowState MarkAsUpdatedInCBS()
        {
            return _flexHost.GetFlexStateInstance<UpdatedInCBS>();
        }

        public override CollectionWorkflowState MarkAsErrorInCBS()
        {
            return _flexHost.GetFlexStateInstance<ErrorInCBS>();
        }

        public override CollectionWorkflowState MarkAsPendingPostingInCBS()
        {
            return _flexHost.GetFlexStateInstance<PendingPostingInCBS>();
        }

        public override CollectionWorkflowState MarkCollectionAsSuccess() => this;

        public override CollectionWorkflowState AddCollectionInPartnerBatch()
        {
            return _flexHost.GetFlexStateInstance<AddedCollectionInPartnerBatch>();
        }

        #endregion State
    }
}