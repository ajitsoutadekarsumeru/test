using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class ProcessCollectionBatchesService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AcknowledgeCollectionBatch(AcknowledgeCollectionBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AcknowledgeCollectionBatchDataPacket, AcknowledgeCollectionBatchSequence, AcknowledgeCollectionBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AcknowledgeCollectionBatchCommand cmd = new AcknowledgeCollectionBatchCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AcknowledgeCollectionBatchResultModel outputResult = new AcknowledgeCollectionBatchResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AcknowledgeCollectionBatchResultModel : DtoBridge
    {
    }
}