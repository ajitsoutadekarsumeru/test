using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CompanyUserApproved : CompanyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public CompanyUserApproved()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override CompanyUserWorkflowState SaveAsDraft() => this;

        public override CompanyUserWorkflowState Approve() => this;

        public override CompanyUserWorkflowState PendingApproval()
        {
            return _flexHost.GetFlexStateInstance<CompanyUserPendingApproval>();
        }

        public override CompanyUserWorkflowState Reject()
        {
            return _flexHost.GetFlexStateInstance<CompanyUserRejected>();
        }

        public override CompanyUserWorkflowState Disable()
        {
            return _flexHost.GetFlexStateInstance<CompanyUserDisabled>();
        }
    }
}