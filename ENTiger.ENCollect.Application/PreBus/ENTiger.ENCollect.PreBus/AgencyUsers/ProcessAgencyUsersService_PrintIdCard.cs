using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> PrintIdCard(PrintIdCardDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<PrintIdCardDataPacket, PrintIdCardSequence, PrintIdCardDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                PrintIdCardCommand cmd = new PrintIdCardCommand
                {
                    Dto = dto,
                    IdCardNumber = packet.IdCardNumber
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                PrintIdCardResultModel outputResult = new PrintIdCardResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class PrintIdCardResultModel : DtoBridge
    {
    }
}