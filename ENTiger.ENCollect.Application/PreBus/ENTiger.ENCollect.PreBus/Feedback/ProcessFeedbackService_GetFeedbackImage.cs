using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetFeedbackImage(GetFeedbackImageDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetFeedbackImageDataPacket, GetFeedbackImageSequence, GetFeedbackImageDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetFeedbackImageCommand cmd = new GetFeedbackImageCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = dto.FileName;
                return cmdResult;
            }
        }
    }

    public class GetFeedbackImageResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}