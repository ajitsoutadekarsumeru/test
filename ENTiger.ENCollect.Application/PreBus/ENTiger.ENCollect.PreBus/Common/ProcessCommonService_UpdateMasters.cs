using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateMasters(UpdateMastersDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateMastersDataPacket, UpdateMastersSequence, UpdateMastersDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateMastersCommand cmd = new UpdateMastersCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateMastersResultModel outputResult = new UpdateMastersResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateMastersResultModel : DtoBridge
    {
    }
}