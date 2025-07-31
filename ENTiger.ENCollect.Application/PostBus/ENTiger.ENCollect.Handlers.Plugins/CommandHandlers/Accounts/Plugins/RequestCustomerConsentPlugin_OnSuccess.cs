using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class RequestCustomerConsentPlugin : FlexiPluginBase, IFlexiPlugin<RequestCustomerConsentPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            
            CustomerConsentRequested @event = new CustomerConsentRequested
            {
                AppContext = _flexAppContext,  //do not remove this line
                ConsentId = _model.Id,
                AccountId = _model.AccountId,
                AppointmentDate = _model.RequestedVisitTime,
                Link = _urlSettings.CaptureCustomerConsent + _model.SecureToken 
            };
                
            await serviceBusContext.Publish(@event);

        }
    }
}