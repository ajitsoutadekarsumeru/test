using Sumeru.Flex;
using ENTiger.ENCollect.AccountsModule.MarkEligibleForSettlementAccountsPlugins;

namespace ENTiger.ENCollect.AccountsModule
{
    public class MarkEligibleForSettlementSequence : FlexiBusinessRuleSequenceBase<MarkEligibleForSettlementDataPacket>
    {
        public MarkEligibleForSettlementSequence()
        {
            this.Add<LoanAccountExistenceValidator>(); this.Add<EligibilityValidator>(); 
        }
    }
}
