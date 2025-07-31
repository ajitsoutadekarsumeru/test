using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyUserSavedAsDraft : AgencyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyUserSavedAsDraft()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override AgencyUserWorkflowState Approve() => this;

        public override AgencyUserWorkflowState ProcessAuthorizationCardRenewal() => this;

        public override AgencyUserWorkflowState Disable() => this;

        public override AgencyUserWorkflowState PrintIDCards() => this;

        public override AgencyUserWorkflowState PendingApproval()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserPendingApproval>();
        }

        public override AgencyUserWorkflowState Reject() => this;

        public override AgencyUserWorkflowState SaveAsDraft() => this;

        public override AgencyUserWorkflowState ApprovePendingPrinting() => this;
    }
}