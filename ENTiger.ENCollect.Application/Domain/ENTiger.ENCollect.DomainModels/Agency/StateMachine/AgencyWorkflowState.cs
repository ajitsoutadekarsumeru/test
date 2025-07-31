using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class AgencyWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual AgencyWorkflowState Approve() => this;

        public virtual AgencyWorkflowState ApproveWithDeferrals() => this;

        public virtual AgencyWorkflowState ExpireContract() => this;

        public virtual AgencyWorkflowState Disable() => this;

        public virtual AgencyWorkflowState PendingApproval() => this;

        public virtual AgencyWorkflowState PendingApprovalWithDeferrals() => this;

        public virtual AgencyWorkflowState Reject() => this;

        public virtual AgencyWorkflowState SaveAsDraft() => this;

        public virtual AgencyWorkflowState InitialiseAgencyWorkflow() => this;
    }
}