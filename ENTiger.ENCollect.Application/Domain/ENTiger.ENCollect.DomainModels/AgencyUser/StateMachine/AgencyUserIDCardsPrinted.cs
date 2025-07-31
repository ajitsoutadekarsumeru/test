using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencyUserIDCardsPrinted : AgencyUserWorkflowState
    {
        private IFlexHost _flexHost;

        public AgencyUserIDCardsPrinted()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override AgencyUserWorkflowState Approve()
        {
            return _flexHost.GetFlexStateInstance<AgencyUserApproved>();
        }

        public override AgencyUserWorkflowState ProcessAuthorizationCardRenewal() => this;

        public override AgencyUserWorkflowState Disable() => this;

        public override AgencyUserWorkflowState PrintIDCards() => this;

        public override AgencyUserWorkflowState PendingApproval() => this;

        public override AgencyUserWorkflowState Reject() => this;

        public override AgencyUserWorkflowState SaveAsDraft() => this;

        public override AgencyUserWorkflowState ApprovePendingPrinting() => this;
    }
}