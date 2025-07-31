using Elastic.Clients.Elasticsearch.Security;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.Messages.Events.License;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class AddCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<AddCompanyUserPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CompanyUserCreatedEvent @event = new CompanyUserCreatedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
            };
            await serviceBusContext.Publish(@event);

            if (_licenseSettings.UserLimitThresholdList.Contains(_userLimitPercentageUsed))
            {
                UserLicenseLimitReachedEvent message = new UserLicenseLimitReachedEvent();
                message.UserType = _model.UserType;
                message.UserId = _model.Id;
                message.AppContext = _flexAppContext;
                //publish message
                await serviceBusContext.Publish(message);
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