using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{

    public class RequestCustomerConsentPostBusSequence : FlexiPluginSequenceBase<RequestCustomerConsentPostBusDataPacket>
    {

        public RequestCustomerConsentPostBusSequence()
        {
            this.Add<RequestCustomerConsentPlugin>(); 
        }
    }
}
