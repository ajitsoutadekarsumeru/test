using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CompanyUserPendingApproval : CompanyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public CompanyUserPendingApproval()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override CompanyUserWorkflowState SaveAsDraft() => this;

        public override CompanyUserWorkflowState Approve()
        {
            return _flexHost.GetFlexStateInstance<CompanyUserApproved>();
        }

        public override CompanyUserWorkflowState PendingApproval() => this;

        public override CompanyUserWorkflowState Reject()
        {
            return _flexHost.GetFlexStateInstance<CompanyUserRejected>();
        }

        public override CompanyUserWorkflowState Disable() => this;
    }
}