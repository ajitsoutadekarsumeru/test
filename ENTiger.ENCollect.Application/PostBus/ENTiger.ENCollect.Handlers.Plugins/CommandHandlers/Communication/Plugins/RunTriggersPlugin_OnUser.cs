using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class RunTriggersPlugin : FlexiPluginBase, IFlexiPlugin<RunTriggersPostBusDataPacket>
    {
        const string CONDITION_ONUSER = "OnUser";

        protected virtual async Task OnUser(IFlexServiceBusContextBridge serviceBusContext)
        {

            UsersIdentifiedEvent @event = new UsersIdentifiedEvent
                {
                    AppContext = _flexAppContext,  //do not remove this line

                    //Add your properties here
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}