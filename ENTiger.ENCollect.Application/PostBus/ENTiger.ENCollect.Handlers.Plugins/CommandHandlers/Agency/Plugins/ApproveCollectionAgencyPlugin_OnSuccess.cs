using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ApproveCollectionAgencyPlugin : FlexiPluginBase, IFlexiPlugin<ApproveCollectionAgencyPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AgencyApproved @event = new AgencyApproved
            {
                AppContext = _flexAppContext,  //do not remove this line
                Ids = _model.Select(a => a.Id).ToList()
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}