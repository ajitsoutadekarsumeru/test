using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class MakeDormantAgencyUserPlugin : FlexiPluginBase, IFlexiPlugin<MakeDormantAgencyUserPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgentDormant @event = new AgentDormant
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = _model.Select(a => a.Id).ToList()
            };

            await serviceBusContext.Publish(@event);
        }
    }
}