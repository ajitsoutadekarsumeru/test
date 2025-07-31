using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateCustomerConsentExpiryPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCustomerConsentExpiryPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CustomerConsentUpdated @event = new CustomerConsentUpdated
            {
                AppContext = _flexAppContext,  //do not remove this line
                //AccountId = _model.AccountId,
                //UserId = _model.UserId,
                //AppointmentDate = _model.RequestedVisitTime,
                //Status = _model.Status,
            };

            await serviceBusContext.Publish(@event);

        }
    }
}
