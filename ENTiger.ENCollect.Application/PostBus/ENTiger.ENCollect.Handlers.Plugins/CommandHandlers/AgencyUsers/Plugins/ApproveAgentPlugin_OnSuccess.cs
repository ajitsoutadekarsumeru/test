using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ApproveAgentPlugin : FlexiPluginBase, IFlexiPlugin<ApproveAgentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgentApproved @event = new AgentApproved
            {
                AppContext = _flexAppContext,  //do not remove this line
                //Add your properties here
                Ids = _model.Select(a => a.Id).ToList()
            };
            await serviceBusContext.Publish(@event);
        }
    }
}