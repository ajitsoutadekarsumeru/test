using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> MobileForgotPassword(MobileForgotPasswordDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<MobileForgotPasswordDataPacket, MobileForgotPasswordSequence, MobileForgotPasswordDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                MobileForgotPasswordCommand cmd = new MobileForgotPasswordCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = packet.Dto;
                return cmdResult;
            }
        }
    }

    public class MobileForgotPasswordResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}