using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdatePrintStatus(UpdatePrintStatusDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdatePrintStatusDataPacket, UpdatePrintStatusSequence, UpdatePrintStatusDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdatePrintStatusCommand cmd = new UpdatePrintStatusCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdatePrintStatusResultModel outputResult = new UpdatePrintStatusResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdatePrintStatusResultModel : DtoBridge
    {
    }
}