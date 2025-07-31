using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> VerifyForgotPasswordOTP(VerifyForgotPasswordOTPDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<VerifyForgotPasswordOTPDataPacket, VerifyForgotPasswordOTPSequence, VerifyForgotPasswordOTPDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //VerifyForgotPasswordOTPCommand cmd = new VerifyForgotPasswordOTPCommand
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

    public class VerifyForgotPasswordOTPResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}