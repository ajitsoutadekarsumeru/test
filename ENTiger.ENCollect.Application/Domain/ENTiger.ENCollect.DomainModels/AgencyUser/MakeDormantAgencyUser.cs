using ENTiger.ENCollect.DomainModels.Enum;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class AgencyUser : ApplicationUser
    {
        public virtual AgencyUser MakeDormantAgencyUser(string userId)
        {
            this.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserDormant>().SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.TransactionSource = TransactionSourceEnum.System.Value;
            this.LastModifiedBy = userId;

            this.SetModified();

            return this;
        }
    }
}
