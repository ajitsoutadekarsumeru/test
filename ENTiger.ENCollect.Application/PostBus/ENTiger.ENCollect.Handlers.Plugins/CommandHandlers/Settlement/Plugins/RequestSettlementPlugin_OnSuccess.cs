using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class RequestSettlementPlugin : FlexiPluginBase, IFlexiPlugin<RequestSettlementPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            ProcessSettlementRequestCommand command = new ProcessSettlementRequestCommand
            {
                WorkflowInstanceId = _wfId,
                DomainId = _model.Id,
                FlexAppContext = _flexAppContext
            };
                    
            await serviceBusContext.Send(command);

        }
    }
}