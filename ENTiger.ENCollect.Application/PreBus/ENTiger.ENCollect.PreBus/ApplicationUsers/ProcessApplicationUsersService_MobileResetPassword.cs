using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> MobileResetPassword(MobileResetPasswordDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<MobileResetPasswordDataPacket, MobileResetPasswordSequence, MobileResetPasswordDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //MobileResetPasswordCommand cmd = new MobileResetPasswordCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                MobileResetPasswordResultModel outputResult = new MobileResetPasswordResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class MobileResetPasswordResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}