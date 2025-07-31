using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> CreateDepositSlip(CreateDepositSlipDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CreateDepositSlipDataPacket, CreateDepositSlipSequence, CreateDepositSlipDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CreateDepositSlipCommand cmd = new CreateDepositSlipCommand
                {
                    Dto = dto,
                    CustomId = await _customUtility.GetNextCustomIdAsync(dto.GetAppContext(), CustomIdEnum.PayinSlip.Value)
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CreateDepositSlipResultModel outputResult = new CreateDepositSlipResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class CreateDepositSlipResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}