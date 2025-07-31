using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionBatchCreated : CollectionBatchWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionBatchCreated()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionBatchWorkflowState CreateCollectionBatch() => this;

        public override CollectionBatchWorkflowState AddCollectionBatchInPayInSlip()
        {
            //return this.GetInstance < AddedCollectionBatchInPayInSlip>();
            return _flexHost.GetFlexStateInstance<AddedCollectionBatchInPayInSlip>();
        }

        public override CollectionBatchWorkflowState Dissolve()
        {
            //return this.GetInstance < Dissolved>();
            return _flexHost.GetFlexStateInstance<Dissolved>();
        }

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatch()
        {
            //return this.GetInstance < CollectionBatchAcknowledged>();
            return _flexHost.GetFlexStateInstance<CollectionBatchAcknowledged>();
        }

        public override CollectionBatchWorkflowState CreateCollectionBatchForPartner() => this;

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatchForPartner() => this;

        #endregion State
    }
}