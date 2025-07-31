using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ChangePassword(ChangePasswordDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ChangePasswordDataPacket, ChangePasswordSequence, ChangePasswordDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ChangePasswordCommand cmd = new ChangePasswordCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ChangePasswordResultModel outputResult = new ChangePasswordResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ChangePasswordResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}