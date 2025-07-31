using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> DeactivateAgent(DeactivateAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DeactivateAgentDataPacket, DeactivateAgentSequence, DeactivateAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DeactivateAgentCommand cmd = new DeactivateAgentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DeactivateAgentResultModel outputResult = new DeactivateAgentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DeactivateAgentResultModel : DtoBridge
    {
    }
}