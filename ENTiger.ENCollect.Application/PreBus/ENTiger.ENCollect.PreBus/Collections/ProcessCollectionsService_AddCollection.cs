using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddCollection(AddCollectionDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddCollectionDataPacket, AddCollectionSequence, AddCollectionDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddCollectionCommand cmd = new AddCollectionCommand
                {
                    Dto = dto,
                    ReceiptId = packet.Receiptid,
                    ReservationId = packet.ReservationId
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddCollectionResultModel outputResult = new AddCollectionResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddCollectionResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}