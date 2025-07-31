using Sumeru.Flex;
using ENTiger.ENCollect.AccountsModule.CustomerConsentResponseAccountsPlugins;

namespace ENTiger.ENCollect.AccountsModule
{
    public class CustomerConsentResponseSequence : FlexiBusinessRuleSequenceBase<CustomerConsentResponseDataPacket>
    {
        public CustomerConsentResponseSequence()
        {         
            this.Add<IsValid>(); 
        }
    }
}
