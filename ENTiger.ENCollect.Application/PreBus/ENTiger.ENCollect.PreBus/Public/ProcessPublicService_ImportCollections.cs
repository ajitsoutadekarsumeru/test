using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ProcessPublicService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ImportCollections(ImportCollectionsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ImportCollectionsDataPacket, ImportCollectionsSequence, ImportCollectionsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ImportCollectionsCommand cmd = new ImportCollectionsCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ImportCollectionsResultModel outputResult = new ImportCollectionsResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class ImportCollectionsResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
