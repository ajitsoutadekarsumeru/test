using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetGeneratedFile(GetGeneratedFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetGeneratedFileDataPacket, GetGeneratedFileSequence, GetGeneratedFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = dto.FileName;
                return cmdResult;
            }
        }
    }

    public class GetGeneratedFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}