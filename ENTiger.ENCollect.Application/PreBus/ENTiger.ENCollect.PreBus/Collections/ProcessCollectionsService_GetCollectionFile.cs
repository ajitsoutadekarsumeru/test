using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetCollectionFile(GetCollectionFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetCollectionFileDataPacket, GetCollectionFileSequence, GetCollectionFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetCollectionFileCommand cmd = new GetCollectionFileCommand
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
    public class GetCollectionFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
