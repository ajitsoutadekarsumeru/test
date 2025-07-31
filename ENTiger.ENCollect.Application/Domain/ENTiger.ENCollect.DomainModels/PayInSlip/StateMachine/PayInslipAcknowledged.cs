using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PayInslipAcknowledged : PayInSlipWorkflowState
    {
        private IFlexHost _flexHost;

        public PayInslipAcknowledged()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override PayInSlipWorkflowState CreatePayInSlip() => this;

        public override PayInSlipWorkflowState AcknowledgePayInSlip() => this;
    }
}