using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateRoleBasedSearch(UpdateAccountScopeConfigurationDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateRoleBasedSearchDataPacket, UpdateRoleBasedSearchSequence, UpdateAccountScopeConfigurationDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateAccountScopeConfigurationCommand cmd = new UpdateAccountScopeConfigurationCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateRoleBasedSearchResultModel outputResult = new UpdateRoleBasedSearchResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateRoleBasedSearchResultModel : DtoBridge
    {
    }
}
