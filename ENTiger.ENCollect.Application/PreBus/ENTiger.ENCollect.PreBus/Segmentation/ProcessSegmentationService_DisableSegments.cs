using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> DisableSegments(DisableSegmentsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DisableSegmentsDataPacket, DisableSegmentsSequence, DisableSegmentsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DisableSegmentsCommand cmd = new DisableSegmentsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DisableSegmentsResultModel outputResult = new DisableSegmentsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DisableSegmentsResultModel : DtoBridge
    {
    }
}