using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ExecuteSegment(ExecuteSegmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ExecuteSegmentDataPacket, ExecuteSegmentSequence, ExecuteSegmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ExecuteSegmentCommand cmd = new ExecuteSegmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ExecuteSegmentResultModel outputResult = new ExecuteSegmentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ExecuteSegmentResultModel : DtoBridge
    {
    }
}