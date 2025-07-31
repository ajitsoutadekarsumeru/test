using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> MobileLogin(MobileLoginDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<MobileLoginDataPacket, MobileLoginSequence, MobileLoginDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //MobileLoginCommand cmd = new MobileLoginCommand
                //{
                //     Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                MobileLoginResultModel outputResult = new MobileLoginResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class MobileLoginResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}