using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PayInSlipCreated : PayInSlipWorkflowState
    {
        private IFlexHost _flexHost;

        public PayInSlipCreated()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override PayInSlipWorkflowState CreatePayInSlip() => this;

        public override PayInSlipWorkflowState AcknowledgePayInSlip()
        {
            return _flexHost.GetFlexStateInstance<PayInslipAcknowledged>();
        }
    }
}