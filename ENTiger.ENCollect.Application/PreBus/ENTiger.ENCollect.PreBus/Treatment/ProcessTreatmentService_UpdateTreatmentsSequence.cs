using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateTreatmentsSequence(UpdateTreatmentsSequenceDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateTreatmentsSequenceDataPacket, UpdateTreatmentsSequenceSequence, UpdateTreatmentsSequenceDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateTreatmentsSequenceCommand cmd = new UpdateTreatmentsSequenceCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateTreatmentsSequenceResultModel outputResult = new UpdateTreatmentsSequenceResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateTreatmentsSequenceResultModel : DtoBridge
    {
    }
}