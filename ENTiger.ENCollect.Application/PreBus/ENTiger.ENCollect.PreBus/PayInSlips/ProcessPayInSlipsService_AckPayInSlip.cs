using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AckPayInSlip(AckPayInSlipDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AckPayInSlipDataPacket, AckPayInSlipSequence, AckPayInSlipDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AckPayInSlipCommand cmd = new AckPayInSlipCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AckPayInSlipResultModel outputResult = new AckPayInSlipResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AckPayInSlipResultModel : DtoBridge
    {
    }
}