using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> DeleteSegments(DeleteSegmentsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DeleteSegmentsDataPacket, DeleteSegmentsSequence, DeleteSegmentsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DeleteSegmentsCommand cmd = new DeleteSegmentsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DeleteSegmentsResultModel outputResult = new DeleteSegmentsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DeleteSegmentsResultModel : DtoBridge
    {
    }
}