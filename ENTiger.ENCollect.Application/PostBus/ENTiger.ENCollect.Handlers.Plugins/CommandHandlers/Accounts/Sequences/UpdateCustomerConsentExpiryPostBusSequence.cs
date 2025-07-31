using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public class UpdateCustomerConsentExpiryPostBusSequence : FlexiPluginSequenceBase<UpdateCustomerConsentExpiryPostBusDataPacket>
    {
        public UpdateCustomerConsentExpiryPostBusSequence()
        {
            this.Add<UpdateCustomerConsentExpiryPlugin>(); 
        }
    }
}
