using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> EnableSegments(EnableSegmentsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<EnableSegmentsDataPacket, EnableSegmentsSequence, EnableSegmentsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                EnableSegmentsCommand cmd = new EnableSegmentsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                EnableSegmentsResultModel outputResult = new EnableSegmentsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class EnableSegmentsResultModel : DtoBridge
    {
    }
}