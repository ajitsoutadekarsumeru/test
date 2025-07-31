using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyUserPendingPrintingApproved : AgencyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyUserPendingPrintingApproved()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override AgencyUserWorkflowState Approve() => this;

        public override AgencyUserWorkflowState ProcessAuthorizationCardRenewal() => this;

        public override AgencyUserWorkflowState Disable() => this;

        public override AgencyUserWorkflowState PrintIDCards()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserIDCardsPrinted>();
        }

        public override AgencyUserWorkflowState PendingApproval() => this;

        public override AgencyUserWorkflowState Reject() => this;

        public override AgencyUserWorkflowState SaveAsDraft() => this;

        public override AgencyUserWorkflowState ApprovePendingPrinting() => this;
    }
}