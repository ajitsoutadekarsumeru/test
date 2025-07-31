using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> VerifyAddNumberOTP(VerifyAddNumberOTPDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<VerifyAddNumberOTPDataPacket, VerifyAddNumberOTPSequence, VerifyAddNumberOTPDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //VerifyAddNumberOTPCommand cmd = new VerifyAddNumberOTPCommand
                //{
                //     Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                VerifyAddNumberOTPResultModel outputResult = new VerifyAddNumberOTPResultModel();
                outputResult = packet.output;
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class VerifyAddNumberOTPResultModel : DtoBridge
    {
        public string Id { get; set; }
        public bool IsOTPVerified { get; set; }
        public string Message { get; set; }
    }
}