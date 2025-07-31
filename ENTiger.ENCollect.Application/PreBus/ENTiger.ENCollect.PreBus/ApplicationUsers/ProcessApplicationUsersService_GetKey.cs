using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetKey(GetKeyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetKeyDataPacket, GetKeySequence, GetKeyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                GetKeyCommand cmd = new GetKeyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                GetKeyResultModel outputResult = new GetKeyResultModel()
                {
                    ReferenceId = this.ReferenceId,
                    Key = this.Key
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class GetKeyResultModel : DtoBridge
    {
        public string? ReferenceId { get; set; }
        public string? Key { get; set; }
    }
}