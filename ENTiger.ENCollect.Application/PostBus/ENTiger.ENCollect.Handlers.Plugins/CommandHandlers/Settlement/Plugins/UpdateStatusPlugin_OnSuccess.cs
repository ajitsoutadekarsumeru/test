using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class UpdateStatusPlugin : FlexiPluginBase, IFlexiPlugin<UpdateStatusPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            StatusUpdated @event = new StatusUpdated
                {
                    AppContext = _flexAppContext,  //do not remove this line

                    //Add your properties here
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}