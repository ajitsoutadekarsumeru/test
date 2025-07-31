using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateLoanAccountFlag(UpdateLoanAccountFlagDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateLoanAccountFlagDataPacket, UpdateLoanAccountFlagSequence, UpdateLoanAccountFlagDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateLoanAccountFlagCommand cmd = new UpdateLoanAccountFlagCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateLoanAccountFlagResultModel outputResult = new UpdateLoanAccountFlagResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateLoanAccountFlagResultModel : DtoBridge
    {
    }
}