using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ApproveAgencyWithDeferralPlugin : FlexiPluginBase, IFlexiPlugin<ApproveAgencyWithDeferralPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            ApproveAgencyWithDefferal @event = new ApproveAgencyWithDefferal
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}