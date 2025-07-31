using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetUnAllocationFile(GetUnAllocationFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetUnAllocationFileDataPacket, GetUnAllocationFileSequence, GetUnAllocationFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetUnAllocationFileCommand cmd = new GetUnAllocationFileCommand
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
    public class GetUnAllocationFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
