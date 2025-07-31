using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> RequestSettlement(RequestSettlementDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RequestSettlementDataPacket, RequestSettlementSequence, RequestSettlementDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RequestSettlementCommand cmd = new RequestSettlementCommand
                {
                     Dto = dto,
                    CustomId = await _customUtility.GetNextCustomIdAsync(dto.GetAppContext(), CustomIdEnum.Settlement.Value)

                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RequestSettlementResultModel outputResult = new RequestSettlementResultModel();
                outputResult.Id = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class RequestSettlementResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
