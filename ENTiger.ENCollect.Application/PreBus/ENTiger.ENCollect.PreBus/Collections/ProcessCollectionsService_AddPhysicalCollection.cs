using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddPhysicalCollection(AddPhysicalCollectionDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddPhysicalCollectionDataPacket, AddPhysicalCollectionSequence, AddPhysicalCollectionDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddPhysicalCollectionCommand cmd = new AddPhysicalCollectionCommand
                {
                    Dto = dto,
                    CustomId = await _customUtility.GetNextCustomIdAsync(dto.GetAppContext(), CustomIdEnum.EReceipt.Value)
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddPhysicalCollectionResultModel outputResult = new AddPhysicalCollectionResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddPhysicalCollectionResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}