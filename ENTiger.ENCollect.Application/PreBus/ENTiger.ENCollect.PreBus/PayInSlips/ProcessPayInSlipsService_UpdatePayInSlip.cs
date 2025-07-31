using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdatePayInSlip(UpdatePayInSlipDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdatePayInSlipDataPacket, UpdatePayInSlipSequence, UpdatePayInSlipDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdatePayInSlipCommand cmd = new UpdatePayInSlipCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdatePayInSlipResultModel outputResult = new UpdatePayInSlipResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdatePayInSlipResultModel : DtoBridge
    {
    }
}