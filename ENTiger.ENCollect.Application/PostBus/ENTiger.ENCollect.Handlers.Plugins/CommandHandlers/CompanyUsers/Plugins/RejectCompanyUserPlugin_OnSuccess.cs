using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class RejectCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<RejectCompanyUserPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CompanyUserRejected @event = new CompanyUserRejected
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Select(a => a.Id).ToList()
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}