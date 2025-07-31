using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddSegment(AddSegmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddSegmentDataPacket, AddSegmentSequence, AddSegmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddSegmentCommand cmd = new AddSegmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddSegmentResultModel outputResult = new AddSegmentResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddSegmentResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}