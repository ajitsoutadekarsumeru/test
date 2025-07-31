using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementCustomerRejectPlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementCustomerRejectPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CustomerRejectedEvent @event = new CustomerRejectedEvent
            {
                WorkflowInstanceId = _model.Id + "_SettlementWorkflow",
                DomainId = _model.Id,
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

            

          

        }
    }
}