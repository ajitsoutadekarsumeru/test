using Sumeru.Flex;
using ENTiger.ENCollect.AccountsModule;

namespace ENTiger.ENCollect
{
    public partial class LoanAccount : DomainModelBridge
    {

        #region "Public Methods"
        public virtual LoanAccount MarkEligibleForSettlement(MarkEligibleForSettlementCommand cmd)
        {
            this.Convert(cmd.Dto);
            
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.IsEligibleForSettlement = true;
            
            this.SetModified();
            
            return this;
        }
        #endregion


        #region "Private Methods"
        #endregion

    }
}
