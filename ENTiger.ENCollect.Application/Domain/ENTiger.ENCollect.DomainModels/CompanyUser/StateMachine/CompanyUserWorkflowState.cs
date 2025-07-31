using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class CompanyUserWorkflowState : FlexState
    {
        public virtual string FriendlyName => "";
        public string Name { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual CompanyUserWorkflowState SaveAsDraft() => this;

        public virtual CompanyUserWorkflowState PendingApproval() => this;

        public virtual CompanyUserWorkflowState Approve() => this;

        public virtual CompanyUserWorkflowState Reject() => this;

        public virtual CompanyUserWorkflowState Disable() => this;

        public virtual CompanyUserWorkflowState Dormant() => this;
    }
}