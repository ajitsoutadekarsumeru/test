using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RejectCollectionCancellation(RejectCollectionCancellationDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RejectCollectionCancellationDataPacket, RejectCollectionCancellationSequence, RejectCollectionCancellationDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RejectCollectionCancellationCommand cmd = new RejectCollectionCancellationCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RejectCollectionCancellationResultModel outputResult = new RejectCollectionCancellationResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RejectCollectionCancellationResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}