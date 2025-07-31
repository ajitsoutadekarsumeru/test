using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> PrimaryAllocationByFilter(PrimaryAllocationByFilterDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<PrimaryAllocationByFilterDataPacket, PrimaryAllocationByFilterSequence, PrimaryAllocationByFilterDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                PrimaryAllocationByFilterCommand cmd = new PrimaryAllocationByFilterCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                PrimaryAllocationByFilterResultModel outputResult = new PrimaryAllocationByFilterResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class PrimaryAllocationByFilterResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}