using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AddAgencyPlugin : FlexiPluginBase, IFlexiPlugin<AddAgencyPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgencyCreatedEvent @event = new AgencyCreatedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}