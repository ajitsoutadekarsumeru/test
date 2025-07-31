using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionBatchAcknowledgedByPartner : CollectionBatchWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionBatchAcknowledgedByPartner()
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
            //return this.GetInstance<AddedCollectionBatchInPayInSlip>();
        }

        public override CollectionBatchWorkflowState Dissolve()
        {
            return _flexHost.GetFlexStateInstance<Dissolved>();
            //return this.GetInstance<Dissolved>();
        }

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatch() => this;

        public override CollectionBatchWorkflowState CreateCollectionBatchForPartner() => this;

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatchForPartner() => this;

        #endregion State
    }
}