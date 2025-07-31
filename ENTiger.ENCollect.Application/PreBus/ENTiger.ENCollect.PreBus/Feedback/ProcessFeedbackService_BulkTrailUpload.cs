using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> BulkTrailUpload(BulkTrailUploadDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<BulkTrailUploadDataPacket, BulkTrailUploadSequence, BulkTrailUploadDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                BulkTrailUploadCommand cmd = new BulkTrailUploadCommand
                {
                    Dto = dto
                    // CustomId = DateTime.Now.ToString("MMddyyyyhhmmssfff")
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                BulkTrailUploadResultModel outputResult = new BulkTrailUploadResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = dto.Customid;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class BulkTrailUploadResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}