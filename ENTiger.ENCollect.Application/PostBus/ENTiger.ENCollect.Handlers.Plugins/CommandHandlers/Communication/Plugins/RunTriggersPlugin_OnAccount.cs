using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class RunTriggersPlugin : FlexiPluginBase, IFlexiPlugin<RunTriggersPostBusDataPacket>
    {
        const string CONDITION_ONACCOUNT = "OnAccount";

        protected virtual async Task OnAccount(IFlexServiceBusContextBridge serviceBusContext)
        {

            AccountsIdentifiedEvent @event = new AccountsIdentifiedEvent
                {
                    AppContext = _flexAppContext,  //do not remove this line
                    ActorIds = actorIds,                    
                    TriggerId = _model?.Id ?? "",

                //Add your properties here
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}