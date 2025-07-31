using ENTiger.ENCollect.DomainModels.Enum;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class CompanyUser : ApplicationUser
    {
        public virtual CompanyUser MakeDormantCompanyUser(string userId)
        {
            this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserDormant>().SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.TransactionSource = TransactionSourceEnum.System.Value;
            this.LastModifiedBy = userId;

            this.SetModified();

            return this;
        }
    }
}
