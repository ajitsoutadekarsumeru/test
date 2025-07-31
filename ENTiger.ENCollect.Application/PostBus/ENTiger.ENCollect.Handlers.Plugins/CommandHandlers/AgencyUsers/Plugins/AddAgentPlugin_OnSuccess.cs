using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.Messages.Events.License;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AddAgentPlugin : FlexiPluginBase, IFlexiPlugin<AddAgentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgentAddedEvent @event = new AgentAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);

            // Check if the current user license usage percentage matches any of the configured threshold values
            if (_licenseSettings.UserLimitThresholdList.Contains(_userLimitPercentageUsed))
            {
                UserLicenseLimitReachedEvent message = new UserLicenseLimitReachedEvent();
                message.UserType = _model.UserType;
                message.UserId = _model.Id;
                message.AppContext = _flexAppContext;
                //publish message
                await serviceBusContext.Publish(@event);
            }

            UserDesignationChangedEvent @userLevelUpdatedEvent = new UserDesignationChangedEvent
            {
                AppContext = _flexAppContext,
                ApplicationUserId = _model.Id,
                DesignationIds = _model.Designation.Select(d => d.DesignationId).ToList()
            };

            await serviceBusContext.Publish(@userLevelUpdatedEvent);
            

        }
    }
}