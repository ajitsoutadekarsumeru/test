using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersActivatePlugin : FlexiPluginBase, IFlexiPlugin<AgencyUsersActivatePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            AgencyUsersActivated @event = new AgencyUsersActivated
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = _model.Select(a => a.Id).ToList()
            };

            await serviceBusContext.Publish(@event);

        }
    }
}