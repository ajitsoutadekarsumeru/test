using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class CustomerConsentResponsePlugin : FlexiPluginBase, IFlexiPlugin<CustomerConsentResponsePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CustomerConsentUpdated @event = new CustomerConsentUpdated
            {
                AppContext = _flexAppContext,  //do not remove this line
                ConsentId = _model.Id,
                AccountId = _model.AccountId,
                UserId = _model.UserId,
                AppointmentDate = _model.RequestedVisitTime,
                Status = _model.Status,
            };

            await serviceBusContext.Publish(@event);

        }
    }
}