using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AzureLogin(AzureLoginDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AzureLoginDataPacket, AzureLoginSequence, AzureLoginDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //AzureLoginCommand cmd = new AzureLoginCommand
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

    public class AzureLoginResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}