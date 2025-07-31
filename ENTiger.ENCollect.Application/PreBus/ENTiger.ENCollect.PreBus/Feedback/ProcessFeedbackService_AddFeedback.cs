using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddFeedback(AddFeedbackDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddFeedbackDataPacket, AddFeedbackSequence, AddFeedbackDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddFeedbackCommand cmd = new AddFeedbackCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddFeedbackResultModel outputResult = new AddFeedbackResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddFeedbackResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}