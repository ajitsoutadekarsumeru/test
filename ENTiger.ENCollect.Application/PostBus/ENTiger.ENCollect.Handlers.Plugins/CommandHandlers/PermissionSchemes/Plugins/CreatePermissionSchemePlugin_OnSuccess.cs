using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class CreatePermissionSchemePlugin : FlexiPluginBase, IFlexiPlugin<CreatePermissionSchemePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            PermissionSchemeAdded @event = new PermissionSchemeAdded
            {
                AppContext = _appContext,  //do not remove this line
                PermissionSchemeChangeLog = _permissionSchemeChangeLog
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);

        }
    }
}