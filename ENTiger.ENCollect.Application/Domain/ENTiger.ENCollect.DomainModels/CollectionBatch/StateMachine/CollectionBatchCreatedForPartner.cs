using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionBatchCreatedForPartner : CollectionBatchWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionBatchCreatedForPartner()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionBatchWorkflowState CreateCollectionBatch() => this;

        public override CollectionBatchWorkflowState AddCollectionBatchInPayInSlip()
        {
            //return this.GetInstance<AddedCollectionBatchInPayInSlip>();
            return _flexHost.GetFlexStateInstance<AddedCollectionBatchInPayInSlip>();
        }

        public override CollectionBatchWorkflowState Dissolve()
        {
            //return this.GetInstance<Dissolved>();
            return _flexHost.GetFlexStateInstance<Dissolved>();
        }

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatch() => this;

        public override CollectionBatchWorkflowState CreateCollectionBatchForPartner() => this;

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatchForPartner()
        {
            //return this.GetInstance<CollectionBatchAcknowledgedByPartner>();
            return _flexHost.GetFlexStateInstance<CollectionBatchAcknowledgedByPartner>();
        }

        #endregion State
    }
}