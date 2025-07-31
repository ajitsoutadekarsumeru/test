using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class ProcessDevicesService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ValidateRegisterDeviceOtp(ValidateRegisterDeviceOtpDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ValidateRegisterDeviceOtpDataPacket, ValidateRegisterDeviceOtpSequence, ValidateRegisterDeviceOtpDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //ValidateRegisterDeviceOtpCommand cmd = new ValidateRegisterDeviceOtpCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ValidateRegisterDeviceOtpResultModel outputResult = new ValidateRegisterDeviceOtpResultModel()
                {
                    Id = dto.GetGeneratedId(),
                    Message = "OTP Verified"
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ValidateRegisterDeviceOtpResultModel : DtoBridge
    {
        public string? Id { get; set; }
        public string? Message { get; set; }
    }
}