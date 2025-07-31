using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementNegotiatePlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementNegotiatePostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            RenegotiationProcessedEvent @event = new RenegotiationProcessedEvent
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _model.Id,
                RequestorId = _model.CreatedBy,
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}