using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementRecommendationPlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementRecommendationPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            SettlementRecommendedEvent @event = new SettlementRecommendedEvent
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _settlementId,
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}