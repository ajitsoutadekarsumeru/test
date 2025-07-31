using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SendVideoCallLink(SendVideoCallLinkDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SendVideoCallLinkDataPacket, SendVideoCallLinkSequence, SendVideoCallLinkDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SendVideoCallLinkCommand cmd = new SendVideoCallLinkCommand
                {
                    Dto = dto,
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SendVideoCallLinkResultModel outputResult = new SendVideoCallLinkResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SendVideoCallLinkResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}