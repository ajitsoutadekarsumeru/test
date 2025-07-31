using Sumeru.Flex;
using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public class AgencyUserDormant : AgencyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyUserDormant()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override AgencyUserWorkflowState Approve()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserApproved>();
        }

        public override AgencyUserWorkflowState Disable() => this;

        public override AgencyUserWorkflowState PendingApproval() => this;

        public override AgencyUserWorkflowState Reject() => this;

    }
}
