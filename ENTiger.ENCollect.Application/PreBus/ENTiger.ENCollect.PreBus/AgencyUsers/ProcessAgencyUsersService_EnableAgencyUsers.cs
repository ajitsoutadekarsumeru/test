using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> EnableAgencyUsers(EnableAgencyUsersDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<EnableAgencyUsersDataPacket, EnableAgencyUsersSequence, EnableAgencyUsersDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                EnableAgencyUsersCommand cmd = new EnableAgencyUsersCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                EnableAgencyUsersResultModel outputResult = new EnableAgencyUsersResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class EnableAgencyUsersResultModel : DtoBridge
    {
    }
}
