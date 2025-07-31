using ENTiger.ENCollect.ApplicationUsersModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class UpdateAgentPlugin : FlexiPluginBase, IFlexiPlugin<UpdateAgentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgentCreatedEvent @event = new AgentCreatedEvent
            {
                AppContext = _flexAppContext
            };

            await serviceBusContext.Publish(@event);            

            UserDesignationChangedEvent @userLevelUpdatedEvent = new UserDesignationChangedEvent
            {
                AppContext = _flexAppContext,
                ApplicationUserId = _model.Id,
                DesignationIds = _model.Designation.Where(a=>a.Designation?.IsDeleted == false).Select(d => d.DesignationId).ToList()
            };

            await serviceBusContext.Publish(@userLevelUpdatedEvent);
        }
    }
}