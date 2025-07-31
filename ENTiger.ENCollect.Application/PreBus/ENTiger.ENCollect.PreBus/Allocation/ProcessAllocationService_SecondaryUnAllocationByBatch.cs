using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SecondaryUnAllocationByBatch(SecondaryUnAllocationByBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SecondaryUnAllocationByBatchDataPacket, SecondaryUnAllocationByBatchSequence, SecondaryUnAllocationByBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SecondaryUnAllocationByBatchCommand cmd = new SecondaryUnAllocationByBatchCommand
                {
                    Dto = dto,
                    CustomId = DateTime.Now.ToString("yyyyMMddhhmmssfff")
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SecondaryUnAllocationByBatchResultModel outputResult = new SecondaryUnAllocationByBatchResultModel();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SecondaryUnAllocationByBatchResultModel : DtoBridge
    {
        public string CustomId { get; set; }
    }
}