using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementCustomerAcceptancePlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementCustomerAcceptancePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CustomerAcceptedEvent @event = new CustomerAcceptedEvent
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _settlementId,
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}