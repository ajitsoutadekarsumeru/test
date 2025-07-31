using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyApproved : AgencyWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyApproved()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region public

        public override AgencyWorkflowState Approve() => this;

        public override AgencyWorkflowState ApproveWithDeferrals() => this;

        public override AgencyWorkflowState ExpireContract()
        {
            return _flexHost.GetFlexStateInstance<AgencyContractExpired>();
        }

        public override AgencyWorkflowState Disable()
        {
            return _flexHost.GetFlexStateInstance<AgencyDisabled>();
        }

        public override AgencyWorkflowState PendingApproval() => this;

        public override AgencyWorkflowState PendingApprovalWithDeferrals() => this;

        public override AgencyWorkflowState Reject()
        {
            return _flexHost.GetFlexStateInstance<AgencyRejected>();
        }

        public override AgencyWorkflowState InitialiseAgencyWorkflow() => this;

        public override AgencyWorkflowState SaveAsDraft() => this;

        #endregion public
    }
}