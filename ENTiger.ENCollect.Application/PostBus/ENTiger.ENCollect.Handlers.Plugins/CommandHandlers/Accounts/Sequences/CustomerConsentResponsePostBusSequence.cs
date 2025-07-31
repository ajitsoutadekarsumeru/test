using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public class CustomerConsentResponsePostBusSequence : FlexiPluginSequenceBase<CustomerConsentResponsePostBusDataPacket>
    {
        public CustomerConsentResponsePostBusSequence()
        {
            this.Add<CustomerConsentResponsePlugin>(); 
        }
    }
}
