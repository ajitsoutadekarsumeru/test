using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyUserPendingApproval : AgencyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyUserPendingApproval()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override AgencyUserWorkflowState Approve()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserApproved>();
        }

        public override AgencyUserWorkflowState ProcessAuthorizationCardRenewal() => this;

        public override AgencyUserWorkflowState Disable() => this;

        public override AgencyUserWorkflowState PrintIDCards() => this;

        public override AgencyUserWorkflowState PendingApproval() => this;

        public override AgencyUserWorkflowState Reject()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserRejected>();
        }

        public override AgencyUserWorkflowState SaveAsDraft() => this;

        public override AgencyUserWorkflowState ApprovePendingPrinting()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserPendingPrintingApproved>();
        }
    }
}