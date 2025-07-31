using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SendPaymentCopyViaEmail(SendPaymentCopyViaEmailDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SendPaymentCopyViaEmailDataPacket, SendPaymentCopyViaEmailSequence, SendPaymentCopyViaEmailDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SendPaymentCopyViaEmailCommand cmd = new SendPaymentCopyViaEmailCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SendPaymentCopyViaEmailResultModel outputResult = new SendPaymentCopyViaEmailResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SendPaymentCopyViaEmailResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}