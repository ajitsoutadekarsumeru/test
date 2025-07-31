using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SecondaryAllocationByBatch(SecondaryAllocationByBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SecondaryAllocationByBatchDataPacket, SecondaryAllocationByBatchSequence, SecondaryAllocationByBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SecondaryAllocationByBatchCommand cmd = new SecondaryAllocationByBatchCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SecondaryAllocationByBatchResultModel outputResult = new SecondaryAllocationByBatchResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = dto.Customid;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SecondaryAllocationByBatchResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}