using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> CancelSettlement(CancelSettlementDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CancelSettlementDataPacket, CancelSettlementSequence, CancelSettlementDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CancelSettlementCommand cmd = new CancelSettlementCommand
                {
                    Dto = dto
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CancelSettlementResultModel outputResult = new CancelSettlementResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class CancelSettlementResultModel : DtoBridge
    {
    }
}
