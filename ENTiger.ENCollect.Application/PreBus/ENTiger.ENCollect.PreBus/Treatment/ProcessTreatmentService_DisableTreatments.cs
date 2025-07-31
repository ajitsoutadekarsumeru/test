using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> DisableTreatments(DisableTreatmentsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DisableTreatmentsDataPacket, DisableTreatmentsSequence, DisableTreatmentsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DisableTreatmentsCommand cmd = new DisableTreatmentsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DisableTreatmentsResultModel outputResult = new DisableTreatmentsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DisableTreatmentsResultModel : DtoBridge
    {
    }
}