using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> PrimaryAllocationByBatch(PrimaryAllocationByBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<PrimaryAllocationByBatchDataPacket, PrimaryAllocationByBatchSequence, PrimaryAllocationByBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                PrimaryAllocationByBatchCommand cmd = new PrimaryAllocationByBatchCommand
                {
                    Dto = dto,
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                PrimaryAllocationByBatchResultModel outputResult = new PrimaryAllocationByBatchResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = dto.Customid;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class PrimaryAllocationByBatchResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}