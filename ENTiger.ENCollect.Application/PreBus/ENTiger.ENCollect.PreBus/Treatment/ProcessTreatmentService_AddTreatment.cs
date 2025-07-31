using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddTreatment(AddTreatmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddTreatmentDataPacket, AddTreatmentSequence, AddTreatmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddTreatmentCommand cmd = new AddTreatmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddTreatmentResultModel outputResult = new AddTreatmentResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddTreatmentResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}