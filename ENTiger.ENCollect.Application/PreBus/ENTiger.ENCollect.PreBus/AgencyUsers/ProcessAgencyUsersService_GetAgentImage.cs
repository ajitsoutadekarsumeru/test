using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetAgentImage(GetAgentImageDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetAgentImageDataPacket, GetAgentImageSequence, GetAgentImageDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetAgentImageCommand cmd = new GetAgentImageCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = dto.FileName;
                return cmdResult;
            }
        }
    }

    public class GetAgentImageResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}