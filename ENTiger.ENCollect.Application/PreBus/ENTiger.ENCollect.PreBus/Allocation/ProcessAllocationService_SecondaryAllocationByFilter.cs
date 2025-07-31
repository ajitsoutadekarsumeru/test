using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SecondaryAllocationByFilter(SecondaryAllocationByFilterDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SecondaryAllocationByFilterDataPacket, SecondaryAllocationByFilterSequence, SecondaryAllocationByFilterDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SecondaryAllocationByFilterCommand cmd = new SecondaryAllocationByFilterCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SecondaryAllocationByFilterResultModel outputResult = new SecondaryAllocationByFilterResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SecondaryAllocationByFilterResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}