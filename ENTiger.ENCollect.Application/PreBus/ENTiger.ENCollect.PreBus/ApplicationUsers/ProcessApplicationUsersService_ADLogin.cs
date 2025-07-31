using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ADLogin(ADLoginDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ADLoginDataPacket, ADLoginSequence, ADLoginDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //ADLoginCommand cmd = new ADLoginCommand
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

    public class ADLoginResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}