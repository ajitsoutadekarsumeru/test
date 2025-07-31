using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateAgent(UpdateAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateAgentDataPacket, UpdateAgentSequence, UpdateAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateAgentCommand cmd = new UpdateAgentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateAgentResultModel outputResult = new UpdateAgentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateAgentResultModel : DtoBridge
    {
    }
}