using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RejectAgent(RejectAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RejectAgentDataPacket, RejectAgentSequence, RejectAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RejectAgentCommand cmd = new RejectAgentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RejectAgentResultModel outputResult = new RejectAgentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RejectAgentResultModel : DtoBridge
    {
    }
}