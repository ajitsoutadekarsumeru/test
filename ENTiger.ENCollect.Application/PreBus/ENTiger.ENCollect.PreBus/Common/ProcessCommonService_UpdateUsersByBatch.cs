using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateUsersByBatch(UpdateUsersByBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateUsersByBatchDataPacket, UpdateUsersByBatchSequence, UpdateUsersByBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateUsersByBatchCommand cmd = new UpdateUsersByBatchCommand
                {
                    Dto = dto,
                    CustomId = DateTime.Now.ToString("yyyyMMddhhmmssfff"),
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateUsersByBatchResultModel outputResult = new UpdateUsersByBatchResultModel();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateUsersByBatchResultModel : DtoBridge
    {
        public string CustomId { get; set; }
    }
}