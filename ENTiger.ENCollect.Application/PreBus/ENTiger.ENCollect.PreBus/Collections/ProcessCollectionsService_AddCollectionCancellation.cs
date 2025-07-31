using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddCollectionCancellation(AddCollectionCancellationDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddCollectionCancellationDataPacket, AddCollectionCancellationSequence, AddCollectionCancellationDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddCollectionCancellationCommand cmd = new AddCollectionCancellationCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddCollectionCancellationResultModel outputResult = new AddCollectionCancellationResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddCollectionCancellationResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}