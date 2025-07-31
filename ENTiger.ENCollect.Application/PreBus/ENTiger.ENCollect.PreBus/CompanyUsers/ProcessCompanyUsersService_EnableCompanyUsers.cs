using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> EnableCompanyUsers(EnableCompanyUsersDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<EnableCompanyUsersDataPacket, EnableCompanyUsersSequence, EnableCompanyUsersDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                EnableCompanyUsersCommand cmd = new EnableCompanyUsersCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                EnableCompanyUsersResultModel outputResult = new EnableCompanyUsersResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class EnableCompanyUsersResultModel : DtoBridge
    {
    }
}
