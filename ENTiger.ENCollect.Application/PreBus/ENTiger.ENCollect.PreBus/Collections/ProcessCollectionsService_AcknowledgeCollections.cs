using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AcknowledgeCollections(AcknowledgeCollectionsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AcknowledgeCollectionsDataPacket, AcknowledgeCollectionsSequence, AcknowledgeCollectionsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AcknowledgeCollectionsCommand cmd = new AcknowledgeCollectionsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AcknowledgeCollectionsResultModel outputResult = new AcknowledgeCollectionsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AcknowledgeCollectionsResultModel : DtoBridge
    {
    }
}