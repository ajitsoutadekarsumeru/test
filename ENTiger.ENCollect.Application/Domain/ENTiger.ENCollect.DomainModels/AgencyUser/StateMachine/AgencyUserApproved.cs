using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyUserApproved : AgencyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyUserApproved()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override AgencyUserWorkflowState Approve() => this;

        public override AgencyUserWorkflowState ProcessAuthorizationCardRenewal() => this;

        public override AgencyUserWorkflowState Disable()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserDisabled>();
        }

        public override AgencyUserWorkflowState PrintIDCards() => this;

        public override AgencyUserWorkflowState PendingApproval() => this;

        public override AgencyUserWorkflowState Reject()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserRejected>();
        }

        public override AgencyUserWorkflowState SaveAsDraft() => this;

        public override AgencyUserWorkflowState ApprovePendingPrinting() => this;
    }
}