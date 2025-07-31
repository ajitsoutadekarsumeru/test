using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RenewAgent(RenewAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RenewAgentDataPacket, RenewAgentSequence, RenewAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RenewAgentCommand cmd = new RenewAgentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RenewAgentResultModel outputResult = new RenewAgentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RenewAgentResultModel : DtoBridge
    {
    }
}