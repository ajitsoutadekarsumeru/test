using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersActivatePlugin : FlexiPluginBase, IFlexiPlugin<CompanyUsersActivatePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CompanyUsersActivated @event = new CompanyUsersActivated
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = _model.Select(a => a.Id).ToList()
            };

            await serviceBusContext.Publish(@event);

        }
    }
}