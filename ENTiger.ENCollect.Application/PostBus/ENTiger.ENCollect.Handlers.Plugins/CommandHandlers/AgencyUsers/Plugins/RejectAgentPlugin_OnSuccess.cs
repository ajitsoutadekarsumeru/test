using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class RejectAgentPlugin : FlexiPluginBase, IFlexiPlugin<RejectAgentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgentRejected @event = new AgentRejected
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = _model.Select(a => a.Id).ToList()
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}