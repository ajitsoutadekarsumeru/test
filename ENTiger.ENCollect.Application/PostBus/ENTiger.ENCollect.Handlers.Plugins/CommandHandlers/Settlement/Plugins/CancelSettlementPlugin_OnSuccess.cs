using ENCollect.Dyna.Workflows;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class CancelSettlementPlugin : FlexiPluginBase, IFlexiPlugin<CancelSettlementPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            SettlementCancelledEvent @event = new SettlementCancelledEvent
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _model.Id,
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);

            RecordPotentialActorsCommand @command = new RecordPotentialActorsCommand
            {
                WorkflowInstanceId = _WorkflowInstanceId,
                DomainId = _model.Id,
                StepName = _stepName,
                StepType = _stepType,
                WorkflowName = "SettlementWorkflow",
                EligibleUserIds =  new List<string>(),
                FlexAppContext = _flexAppContext
            };

            await serviceBusContext.Send(@command);

        }
    }
}