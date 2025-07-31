using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyContractExpired : AgencyWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyContractExpired()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region public

        public override AgencyWorkflowState Approve()
        {
            return _flexHost.GetFlexStateInstance<AgencyApproved>();
        }

        public override AgencyWorkflowState ApproveWithDeferrals()
        {
            return _flexHost.GetFlexStateInstance<AgencyApprovedWithDeferrals>();
        }

        public override AgencyWorkflowState ExpireContract() => this;

        public override AgencyWorkflowState Disable() => this;

        public override AgencyWorkflowState PendingApproval() => this;

        public override AgencyWorkflowState PendingApprovalWithDeferrals() => this;

        public override AgencyWorkflowState Reject() => this;

        public override AgencyWorkflowState SaveAsDraft() => this;

        public override AgencyWorkflowState InitialiseAgencyWorkflow() => this;

        #endregion public
    }
}