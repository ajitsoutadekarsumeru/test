using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class MarkEligibleForSettlementPlugin : FlexiPluginBase, IFlexiPlugin<MarkEligibleForSettlementPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            MarkEligibleForSettlementCompleted @event = new MarkEligibleForSettlementCompleted
                {
                    AppContext = _flexAppContext,  //do not remove this line

                    //TODO : publish event for each list item
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}