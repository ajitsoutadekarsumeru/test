using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateACM(UpdateACMDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateACMDataPacket, UpdateACMSequence, UpdateACMDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateACMCommand cmd = new UpdateACMCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateACMResultModel outputResult = new UpdateACMResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateACMResultModel : DtoBridge
    {
    }
}