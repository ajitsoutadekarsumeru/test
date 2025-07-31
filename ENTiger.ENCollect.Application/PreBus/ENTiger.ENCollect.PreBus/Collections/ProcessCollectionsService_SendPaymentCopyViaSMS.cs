using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SendPaymentCopyViaSMS(SendPaymentCopyViaSMSDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SendPaymentCopyViaSMSDataPacket, SendPaymentCopyViaSMSSequence, SendPaymentCopyViaSMSDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SendPaymentCopyViaSMSCommand cmd = new SendPaymentCopyViaSMSCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SendPaymentCopyViaSMSResultModel outputResult = new SendPaymentCopyViaSMSResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SendPaymentCopyViaSMSResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}