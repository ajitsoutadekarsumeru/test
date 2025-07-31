using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CompanyUserRejected : CompanyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public CompanyUserRejected()
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

        public override CompanyUserWorkflowState PendingApproval()
        {
            return _flexHost.GetFlexStateInstance<CompanyUserPendingApproval>();
        }

        public override CompanyUserWorkflowState Reject() => this;

        public override CompanyUserWorkflowState Disable() => this;
    }
}