using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> CompanyUsersActivate(CompanyUsersActivateDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CompanyUsersActivateDataPacket, CompanyUsersActivateSequence, CompanyUsersActivateDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CompanyUsersActivateCommand cmd = new CompanyUsersActivateCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CompanyUsersActivateResultModel outputResult = new CompanyUsersActivateResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class CompanyUsersActivateResultModel : DtoBridge
    {
    }
}
