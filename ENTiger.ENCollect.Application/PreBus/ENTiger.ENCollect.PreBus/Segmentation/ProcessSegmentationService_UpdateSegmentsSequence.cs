using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateSegmentsSequence(UpdateSegmentsSequenceDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateSegmentsSequenceDataPacket, UpdateSegmentsSequenceSequence, UpdateSegmentsSequenceDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                // dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateSegmentsSequenceCommand cmd = new UpdateSegmentsSequenceCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateSegmentsSequenceResultModel outputResult = new UpdateSegmentsSequenceResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateSegmentsSequenceResultModel : DtoBridge
    {
    }
}