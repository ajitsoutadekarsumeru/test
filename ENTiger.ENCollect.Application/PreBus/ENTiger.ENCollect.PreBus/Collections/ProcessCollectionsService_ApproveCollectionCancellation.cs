using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ApproveCollectionCancellation(ApproveCollectionCancellationDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ApproveCollectionCancellationDataPacket, ApproveCollectionCancellationSequence, ApproveCollectionCancellationDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ApproveCollectionCancellationCommand cmd = new ApproveCollectionCancellationCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ApproveCollectionCancellationResultModel outputResult = new ApproveCollectionCancellationResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ApproveCollectionCancellationResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}