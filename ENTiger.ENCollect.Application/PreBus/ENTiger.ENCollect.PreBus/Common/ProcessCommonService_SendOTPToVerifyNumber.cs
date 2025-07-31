using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SendOTPToVerifyNumber(SendOTPToVerifyNumberDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SendOTPToVerifyNumberDataPacket, SendOTPToVerifyNumberSequence, SendOTPToVerifyNumberDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //SendOTPToVerifyNumberCommand cmd = new SendOTPToVerifyNumberCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SendOTPToVerifyNumberResultModel outputResult = new SendOTPToVerifyNumberResultModel();
                outputResult = packet.output;
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SendOTPToVerifyNumberResultModel : DtoBridge
    {
        public string Id { get; set; }
        public bool IsOTPVerified { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }
}