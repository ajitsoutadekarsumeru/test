using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetTrailFile(GetTrailFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetTrailFileDataPacket, GetTrailFileSequence, GetTrailFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetTrailFileCommand cmd = new GetTrailFileCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = packet.FilePath;
                return cmdResult;
            }
        }
    }
    public class GetTrailFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
