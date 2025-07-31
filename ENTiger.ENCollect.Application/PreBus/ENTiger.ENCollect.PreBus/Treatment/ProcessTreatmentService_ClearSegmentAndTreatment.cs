using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ClearSegmentAndTreatment(ClearSegmentAndTreatmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ClearSegmentAndTreatmentDataPacket, ClearSegmentAndTreatmentSequence, ClearSegmentAndTreatmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ClearSegmentAndTreatmentCommand cmd = new ClearSegmentAndTreatmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ClearSegmentAndTreatmentResultModel outputResult = new ClearSegmentAndTreatmentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ClearSegmentAndTreatmentResultModel : DtoBridge
    {
    }
}