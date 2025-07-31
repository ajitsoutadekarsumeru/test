using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> SendAccountMessage(SendAccountMessageDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SendAccountMessageDataPacket, SendAccountMessageSequence, SendAccountMessageDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SendAccountMessageCommand cmd = new SendAccountMessageCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SendAccountMessageResultModel outputResult = new SendAccountMessageResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SendAccountMessageResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}