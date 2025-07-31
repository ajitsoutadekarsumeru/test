using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetFile(GetFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetFileDataPacket, GetFileSequence, GetFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetFileCommand cmd = new GetFileCommand
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

    public class GetFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}