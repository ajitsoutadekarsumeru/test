using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ApproveAgent(ApproveAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ApproveAgentDataPacket, ApproveAgentSequence, ApproveAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ApproveAgentCommand cmd = new ApproveAgentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ApproveAgentResultModel outputResult = new ApproveAgentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ApproveAgentResultModel : DtoBridge
    {
    }
}