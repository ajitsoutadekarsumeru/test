using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ValidateAgentMobile(ValidateAgentMobileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ValidateAgentMobileDataPacket, ValidateAgentMobileSequence, ValidateAgentMobileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //ValidateAgentMobileCommand cmd = new ValidateAgentMobileCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                //ValidateAgentMobileResultModel outputResult = new ValidateAgentMobileResultModel();
                //outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = packet.OutputDto;
                return cmdResult;
            }
        }
    }

    public class ValidateAgentMobileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}