using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementApprovalPlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementApprovalPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            SettlementApprovedEvent @event = new SettlementApprovedEvent
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _model.Id,
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}