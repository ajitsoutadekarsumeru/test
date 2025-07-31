using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class RegisterDevicePlugin : FlexiPluginBase, IFlexiPlugin<RegisterDevicePostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            DeviceRegistered @event = new DeviceRegistered
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}