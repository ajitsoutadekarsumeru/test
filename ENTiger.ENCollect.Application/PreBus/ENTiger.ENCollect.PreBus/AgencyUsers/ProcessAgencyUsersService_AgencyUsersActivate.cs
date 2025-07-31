using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AgencyUsersActivate(AgencyUsersActivateDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AgencyUsersActivateDataPacket, AgencyUsersActivateSequence, AgencyUsersActivateDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AgencyUsersActivateCommand cmd = new AgencyUsersActivateCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AgencyUsersActivateResultModel outputResult = new AgencyUsersActivateResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class AgencyUsersActivateResultModel : DtoBridge
    {
    }
}
