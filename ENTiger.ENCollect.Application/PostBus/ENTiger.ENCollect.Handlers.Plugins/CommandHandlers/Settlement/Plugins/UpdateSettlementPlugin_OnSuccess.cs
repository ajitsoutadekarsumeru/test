using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class UpdateSettlementPlugin : FlexiPluginBase, IFlexiPlugin<UpdateSettlementPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            SettlementUpdatedEvent @event = new SettlementUpdatedEvent
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _model.Id,                 
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}