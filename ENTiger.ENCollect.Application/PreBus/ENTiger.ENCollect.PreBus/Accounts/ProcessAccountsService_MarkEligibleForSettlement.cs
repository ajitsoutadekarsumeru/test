using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> MarkEligibleForSettlement(MarkEligibleForSettlementDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<MarkEligibleForSettlementDataPacket, MarkEligibleForSettlementSequence, MarkEligibleForSettlementDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                MarkEligibleForSettlementCommand cmd = new MarkEligibleForSettlementCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                MarkEligibleForSettlementResultModel outputResult = new MarkEligibleForSettlementResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class MarkEligibleForSettlementResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
