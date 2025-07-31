using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> DeleteTreatments(DeleteTreatmentsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DeleteTreatmentsDataPacket, DeleteTreatmentsSequence, DeleteTreatmentsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DeleteTreatmentsCommand cmd = new DeleteTreatmentsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DeleteTreatmentsResultModel outputResult = new DeleteTreatmentsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DeleteTreatmentsResultModel : DtoBridge
    {
    }
}