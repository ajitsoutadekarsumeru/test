using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddAgent(AddAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddAgentDataPacket, AddAgentSequence, AddAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddAgentCommand cmd = new AddAgentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddAgentResultModel outputResult = new AddAgentResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddAgentResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}