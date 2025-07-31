using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateStatus(UpdateStatusDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateStatusDataPacket, UpdateStatusSequence, UpdateStatusDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateStatusCommand cmd = new UpdateStatusCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateStatusResultModel outputResult = new UpdateStatusResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateStatusResultModel : DtoBridge
    {
    }
}
