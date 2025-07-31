using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> CollectionBulkUpload(CollectionBulkUploadDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CollectionBulkUploadDataPacket, CollectionBulkUploadSequence, CollectionBulkUploadDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CollectionBulkUploadCommand cmd = new CollectionBulkUploadCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CollectionBulkUploadResultModel outputResult = new CollectionBulkUploadResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = dto.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class CollectionBulkUploadResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}
