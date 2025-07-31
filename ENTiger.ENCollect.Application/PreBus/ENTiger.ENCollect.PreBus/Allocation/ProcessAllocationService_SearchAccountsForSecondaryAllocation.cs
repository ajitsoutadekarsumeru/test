using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SearchAccountsForSecondaryAllocation(SearchAccountsForSecondaryAllocationDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SearchAccountsForSecondaryAllocationDataPacket, SearchAccountsForSecondaryAllocationSequence, SearchAccountsForSecondaryAllocationDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                SearchAccountsForSecondaryAllocationCommand cmd = new SearchAccountsForSecondaryAllocationCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                SearchAccountsForSecondaryAllocationResultModel outputResult = new SearchAccountsForSecondaryAllocationResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class SearchAccountsForSecondaryAllocationResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}