using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> PrimaryUnAllocationByBatch(PrimaryUnAllocationByBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<PrimaryUnAllocationByBatchDataPacket, PrimaryUnAllocationByBatchSequence, PrimaryUnAllocationByBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                PrimaryUnAllocationByBatchCommand cmd = new PrimaryUnAllocationByBatchCommand
                {
                    Dto = dto,
                    CustomId = DateTime.Now.ToString("yyyyMMddhhmmssfff")
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                PrimaryUnAllocationByBatchResultModel outputResult = new PrimaryUnAllocationByBatchResultModel();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class PrimaryUnAllocationByBatchResultModel : DtoBridge
    {
        public string CustomId { get; set; }
    }
}