using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ValidateAgent(ValidateAgentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ValidateAgentDataPacket, ValidateAgentSequence, ValidateAgentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //ValidateAgentCommand cmd = new ValidateAgentCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                //ValidateAgentResultModel outputResult = new ValidateAgentResultModel();
                //outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = packet.OutputDto;
                return cmdResult;
            }
        }
    }

    public class ValidateAgentResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}