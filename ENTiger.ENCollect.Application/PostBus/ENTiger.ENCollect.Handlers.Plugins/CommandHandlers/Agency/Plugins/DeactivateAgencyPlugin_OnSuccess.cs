using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class DeactivateAgencyPlugin : FlexiPluginBase, IFlexiPlugin<DeactivateAgencyPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgencyDisabled @event = new AgencyDisabled
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}