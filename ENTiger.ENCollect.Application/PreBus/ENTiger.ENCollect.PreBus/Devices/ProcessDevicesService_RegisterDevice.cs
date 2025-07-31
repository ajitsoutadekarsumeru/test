using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class ProcessDevicesService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RegisterDevice(RegisterDeviceDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RegisterDeviceDataPacket, RegisterDeviceSequence, RegisterDeviceDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RegisterDeviceCommand cmd = new RegisterDeviceCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RegisterDeviceResultModel outputResult = new RegisterDeviceResultModel()
                {
                    Id = dto.GetGeneratedId(),
                    Message = packet.message ?? "OTP Sent"
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RegisterDeviceResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
}