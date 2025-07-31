using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ApproveCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<ApproveCompanyUserPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CompanyUserApproved @event = new CompanyUserApproved
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = Ids
            };
            await serviceBusContext.Publish(@event);
        }
    }
}