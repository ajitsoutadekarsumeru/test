using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ForgotPasswordDataPacket, ForgotPasswordSequence, ForgotPasswordDto, FlexAppContextBridge>(dto);
            
            if (!packet.HasError)
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ForgotPasswordCommand cmd = new ForgotPasswordCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);
            }

            CommandResult cmdResult = new CommandResult(Status.Success);
            ForgotPasswordResultModel outputResult = new ForgotPasswordResultModel();
            outputResult.Id = dto.GetGeneratedId();
            cmdResult.result = outputResult;
            return cmdResult;
        }
    }

    public class ForgotPasswordResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}