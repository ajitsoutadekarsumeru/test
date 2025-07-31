using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateSegmentFlag(UpdateSegmentFlagDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateSegmentFlagDataPacket, UpdateSegmentFlagSequence, UpdateSegmentFlagDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateSegmentFlagCommand cmd = new UpdateSegmentFlagCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateSegmentFlagResultModel outputResult = new UpdateSegmentFlagResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateSegmentFlagResultModel : DtoBridge
    {
    }
}