using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class UpdatePermissionSchemePlugin : FlexiPluginBase, IFlexiPlugin<UpdatePermissionSchemePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            PermissionSchemeUpdated @event = new PermissionSchemeUpdated
            {
                AppContext = _appContext,  //do not remove this line
                PermissionSchemeChangeLog = _permissionSchemeChangeLog
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);

        }
    }
}