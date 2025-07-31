using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateSettlement(UpdateSettlementDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateSettlementDataPacket, UpdateSettlementSequence, UpdateSettlementDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateSettlementCommand cmd = new UpdateSettlementCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateSettlementResultModel outputResult = new UpdateSettlementResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateSettlementResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
