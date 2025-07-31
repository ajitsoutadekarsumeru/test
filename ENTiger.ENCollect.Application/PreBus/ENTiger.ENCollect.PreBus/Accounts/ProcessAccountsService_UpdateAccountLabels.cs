using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateAccountLabels(UpdateAccountLabelsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateAccountLabelsDataPacket, UpdateAccountLabelsSequence, UpdateAccountLabelsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateAccountLabelsCommand cmd = new UpdateAccountLabelsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateAccountLabelsResultModel outputResult = new UpdateAccountLabelsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateAccountLabelsResultModel : DtoBridge
    {
    }
}