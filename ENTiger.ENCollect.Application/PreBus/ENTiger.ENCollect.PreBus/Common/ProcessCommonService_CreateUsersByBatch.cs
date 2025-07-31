using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> CreateUsersByBatch(CreateUsersByBatchDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CreateUsersByBatchDataPacket, CreateUsersByBatchSequence, CreateUsersByBatchDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CreateUsersByBatchCommand cmd = new CreateUsersByBatchCommand
                {
                    Dto = dto,
                    CustomId = DateTime.Now.ToString("yyyyMMddhhmmssfff")
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CreateUsersByBatchResultModel outputResult = new CreateUsersByBatchResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = cmd.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class CreateUsersByBatchResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}