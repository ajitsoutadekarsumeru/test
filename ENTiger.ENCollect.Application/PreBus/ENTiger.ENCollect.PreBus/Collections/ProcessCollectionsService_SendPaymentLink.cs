using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SendPaymentLink(SendPaymentLinkDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SendPaymentLinkDataPacket, SendPaymentLinkSequence, SendPaymentLinkDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SendPaymentLinkCommand cmd = new SendPaymentLinkCommand
                {
                    Dto = dto,
                    CustomId = packet.ReceiptNo
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                //SendPaymentLinkResultModel outputResult = new SendPaymentLinkResultModel();
                //outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = packet.ReceiptNo;
                return cmdResult;
            }
        }
    }

    public class SendPaymentLinkResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}