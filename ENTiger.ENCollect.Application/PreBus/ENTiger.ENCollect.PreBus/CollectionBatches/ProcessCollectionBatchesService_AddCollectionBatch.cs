using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class ProcessCollectionBatchesService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddCollectionBatch(AddCollectionBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddCollectionBatchDataPacket, AddCollectionBatchSequence, AddCollectionBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddCollectionBatchCommand cmd = new AddCollectionBatchCommand
                {
                    Dto = dto,
                    CustomId = await _customUtility.GetNextCustomIdAsync(dto.GetAppContext(), CustomIdEnum.CollectionBatch.Value)
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddCollectionBatchResultModel outputResult = new AddCollectionBatchResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddCollectionBatchResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}