using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class RecordPotentialActorsPlugin : FlexiPluginBase, IFlexiPlugin<RecordPotentialActorsPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
           foreach(var actor in _actors)
            {
                ActorIndentifiedEvent command = new ActorIndentifiedEvent
                {
                    WorkflowInstanceId = _workflowInstanceId,
                    DomainId = _settlementId,
                    ApplicationUserId = actor,
                    AppContext = _flexAppContext
                };
                await serviceBusContext.Publish(command);
            }
        }
    }
}