using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateTriggerStatus(UpdateTriggerStatusDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateTriggerStatusDataPacket, UpdateTriggerStatusSequence, UpdateTriggerStatusDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateTriggerStatusCommand cmd = new UpdateTriggerStatusCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateTriggerStatusResultModel outputResult = new UpdateTriggerStatusResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateTriggerStatusResultModel : DtoBridge
    {
    }
}
