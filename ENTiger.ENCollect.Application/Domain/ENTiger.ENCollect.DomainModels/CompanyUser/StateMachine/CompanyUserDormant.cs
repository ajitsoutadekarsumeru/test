using Sumeru.Flex;
using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public class CompanyUserDormant : CompanyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public CompanyUserDormant()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

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
