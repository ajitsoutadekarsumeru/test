using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SearchAccountsForPrimaryAllocation(SearchAccountsForPrimaryAllocationDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SearchAccountsForPrimaryAllocationDataPacket, SearchAccountsForPrimaryAllocationSequence, SearchAccountsForPrimaryAllocationDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SearchAccountsForPrimaryAllocationCommand cmd = new SearchAccountsForPrimaryAllocationCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SearchAccountsForPrimaryAllocationResultModel outputResult = new SearchAccountsForPrimaryAllocationResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SearchAccountsForPrimaryAllocationResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}