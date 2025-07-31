using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> EnableTreatments(EnableTreatmentsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<EnableTreatmentsDataPacket, EnableTreatmentsSequence, EnableTreatmentsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                EnableTreatmentsCommand cmd = new EnableTreatmentsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                EnableTreatmentsResultModel outputResult = new EnableTreatmentsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class EnableTreatmentsResultModel : DtoBridge
    {
    }
}