using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class AddGeoTagPlugin : FlexiPluginBase, IFlexiPlugin<AddGeoTagPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            GeoTagAdded @event = new GeoTagAdded
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}