using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class CreateDepositSlipPlugin : FlexiPluginBase, IFlexiPlugin<CreateDepositSlipPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            PayInSlipCreatedEvent @event = new PayInSlipCreatedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
                Id = _model.Id
            };

            await serviceBusContext.Publish(@event);
        }
    }
}