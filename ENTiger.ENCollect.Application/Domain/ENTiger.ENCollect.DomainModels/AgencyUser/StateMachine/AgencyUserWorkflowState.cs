using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class AgencyUserWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual AgencyUserWorkflowState Approve() => this;

        public virtual AgencyUserWorkflowState Disable() => this;

        public virtual AgencyUserWorkflowState PrintIDCards() => this;

        public virtual AgencyUserWorkflowState PendingApproval() => this;

        public virtual AgencyUserWorkflowState Reject() => this;

        public virtual AgencyUserWorkflowState SaveAsDraft() => this;

        public virtual AgencyUserWorkflowState ProcessAuthorizationCardRenewal() => this;

        public virtual AgencyUserWorkflowState ApprovePendingPrinting() => this;

        public virtual AgencyUserWorkflowState Dormant() => this;
    }
}