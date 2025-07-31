using ENTiger.ENCollect.ApplicationUsersModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class UpdateCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCompanyUserPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CompanyUserCreatedEvent @event = new CompanyUserCreatedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                //Add your properties here
            };
            await serviceBusContext.Publish(@event);



            UserDesignationChangedEvent @userLevelUpdatedEvent = new UserDesignationChangedEvent
            {
                AppContext = _flexAppContext,
                ApplicationUserId = _model.Id,
                DesignationIds = _model.Designation.Where(a => a.Designation.IsDeleted == false).Select(d => d.DesignationId).ToList()
            };

            await serviceBusContext.Publish(@userLevelUpdatedEvent);
        }
    }
}