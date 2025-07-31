using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ValidateAgentEmail(ValidateAgentEmailDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ValidateAgentEmailDataPacket, ValidateAgentEmailSequence, ValidateAgentEmailDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //ValidateAgentEmailCommand cmd = new ValidateAgentEmailCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                //ValidateAgentEmailResultModel outputResult = new ValidateAgentEmailResultModel();
                //outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = packet.OutputDto;
                return cmdResult;
            }
        }
    }

    public class ValidateAgentEmailResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}