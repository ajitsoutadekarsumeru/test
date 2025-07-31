using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateTrigger(UpdateTriggerDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateTriggerDataPacket, UpdateTriggerSequence, UpdateTriggerDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateTriggerCommand cmd = new UpdateTriggerCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateTriggerResultModel outputResult = new UpdateTriggerResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateTriggerResultModel : DtoBridge
    {
    }
}
