using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetAllocationFile(GetAllocationFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetAllocationFileDataPacket, GetAllocationFileSequence, GetAllocationFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetAllocationFileCommand cmd = new GetAllocationFileCommand
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
    public class GetAllocationFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
