using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class ProcessCollectionBatchesService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateCollectionBatch(UpdateCollectionBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateCollectionBatchDataPacket, UpdateCollectionBatchSequence, UpdateCollectionBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateCollectionBatchCommand cmd = new UpdateCollectionBatchCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateCollectionBatchResultModel outputResult = new UpdateCollectionBatchResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateCollectionBatchResultModel : DtoBridge
    {
    }
}