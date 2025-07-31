using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateSegment(UpdateSegmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateSegmentDataPacket, UpdateSegmentSequence, UpdateSegmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateSegmentCommand cmd = new UpdateSegmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateSegmentResultModel outputResult = new UpdateSegmentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateSegmentResultModel : DtoBridge
    {
    }
}