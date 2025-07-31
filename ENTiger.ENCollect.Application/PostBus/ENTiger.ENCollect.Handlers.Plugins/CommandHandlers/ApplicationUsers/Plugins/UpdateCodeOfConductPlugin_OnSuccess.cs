using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class UpdateCodeOfConductPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCodeOfConductPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CodeOfConductUpdated @event = new CodeOfConductUpdated
                {
                    AppContext = _flexAppContext,  //do not remove this line

                    //Add your properties here
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}