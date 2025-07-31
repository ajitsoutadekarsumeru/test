using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountScopeConfigurationPlugin : FlexiPluginBase, IFlexiPlugin<UpdateAccountScopeConfigurationPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            RoleBasedSearchUpdated @event = new RoleBasedSearchUpdated
                {
                    AppContext = _flexAppContext,  //do not remove this line

                    //Add your properties here
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}