using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> VerifyLoginOTP(VerifyLoginOTPDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<VerifyLoginOTPDataPacket, VerifyLoginOTPSequence, VerifyLoginOTPDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //VerifyLoginOTPCommand cmd = new VerifyLoginOTPCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = packet.OutputDto;
                return cmdResult;
            }
        }
    }

    public class VerifyLoginOTPResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}