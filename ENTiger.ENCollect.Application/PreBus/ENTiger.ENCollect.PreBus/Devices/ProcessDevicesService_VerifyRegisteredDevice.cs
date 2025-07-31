using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class ProcessDevicesService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> VerifyRegisteredDevice(VerifyRegisteredDeviceDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<VerifyRegisteredDeviceDataPacket, VerifyRegisteredDeviceSequence, VerifyRegisteredDeviceDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //VerifyRegisteredDeviceCommand cmd = new VerifyRegisteredDeviceCommand
                //{
                //     Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                VerifyRegisteredDeviceResultModel outputResult = new VerifyRegisteredDeviceResultModel()
                {
                    Id = dto.GetGeneratedId(),
                    Email = dto.Email,
                    IMEI = dto.IMEI
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class VerifyRegisteredDeviceResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string IMEI { get; set; }
    }
}