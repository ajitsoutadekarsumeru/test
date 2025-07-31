using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> CreatePayInSlip(CreatePayInSlipDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CreatePayInSlipDataPacket, CreatePayInSlipSequence, CreatePayInSlipDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CreatePayInSlipCommand cmd = new CreatePayInSlipCommand
                {
                    Dto = dto,
                    CustomId = await _customUtility.GetNextCustomIdAsync(dto.GetAppContext(), CustomIdEnum.PayinSlip.Value)
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CreatePayInSlipResultModel outputResult = new CreatePayInSlipResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class CreatePayInSlipResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}