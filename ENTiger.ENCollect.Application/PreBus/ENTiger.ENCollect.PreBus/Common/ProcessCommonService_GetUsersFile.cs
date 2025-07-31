using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetUsersFile(GetUsersFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetUsersFileDataPacket, GetUsersFileSequence, GetUsersFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetUsersFileCommand cmd = new GetUsersFileCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = packet.FilePath;
                return cmdResult;
            }
        }
    }

    public class GetUsersFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}