using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionBatchAcknowledged : CollectionBatchWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionBatchAcknowledged()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionBatchWorkflowState CreateCollectionBatch() => this;

        public override CollectionBatchWorkflowState AddCollectionBatchInPayInSlip()
        {
            return _flexHost.GetFlexStateInstance<AddedCollectionBatchInPayInSlip>();
        }

        public override CollectionBatchWorkflowState Dissolve()
        {
            // return this.GetInstance<Dissolved>(stateChange);
            return _flexHost.GetFlexStateInstance<Dissolved>();
        }

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatch() => this;

        public override CollectionBatchWorkflowState CreateCollectionBatchForPartner() => this;

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatchForPartner() => this;

        #endregion State
    }
}