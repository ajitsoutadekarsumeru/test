using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencySavedAsDraft : AgencyWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencySavedAsDraft()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region public

        public override AgencyWorkflowState Approve() => this;

        public override AgencyWorkflowState ApproveWithDeferrals() => this;

        public override AgencyWorkflowState ExpireContract() => this;

        public override AgencyWorkflowState Disable() => this;

        public override AgencyWorkflowState PendingApproval()
        {
            return _flexHost.GetFlexStateInstance<AgencyPendingApproval>();
        }

        public override AgencyWorkflowState PendingApprovalWithDeferrals()
        {
            return _flexHost.GetFlexStateInstance<AgencyPendingApprovalWithDeferrals>();
        }

        public override AgencyWorkflowState Reject() => this;

        public override AgencyWorkflowState SaveAsDraft() => this;

        public override AgencyWorkflowState InitialiseAgencyWorkflow() => this;

        #endregion public
    }
}