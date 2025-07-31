using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ResetPassword(ResetPasswordDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ResetPasswordDataPacket, ResetPasswordSequence, ResetPasswordDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //ResetPasswordCommand cmd = new ResetPasswordCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ResetPasswordResultModel outputResult = new ResetPasswordResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ResetPasswordResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}