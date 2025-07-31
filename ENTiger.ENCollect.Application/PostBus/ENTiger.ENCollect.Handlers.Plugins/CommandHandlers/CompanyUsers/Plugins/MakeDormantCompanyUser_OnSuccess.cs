using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class MakeDormantCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<MakeDormantCompanyUserPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CompanyUserDormant @event = new CompanyUserDormant
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = _model.Select(a => a.Id).ToList()
            };

            await serviceBusContext.Publish(@event);
        }
    }
}