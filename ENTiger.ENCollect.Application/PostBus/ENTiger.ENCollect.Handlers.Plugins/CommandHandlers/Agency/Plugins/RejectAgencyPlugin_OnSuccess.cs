using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class RejectAgencyPlugin : FlexiPluginBase, IFlexiPlugin<RejectAgencyPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgencyRejected @event = new AgencyRejected
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}