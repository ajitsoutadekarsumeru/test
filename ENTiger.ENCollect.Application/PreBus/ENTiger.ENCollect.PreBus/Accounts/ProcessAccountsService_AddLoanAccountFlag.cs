using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddLoanAccountFlag(AddLoanAccountFlagDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddLoanAccountFlagDataPacket, AddLoanAccountFlagSequence, AddLoanAccountFlagDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddLoanAccountFlagCommand cmd = new AddLoanAccountFlagCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddLoanAccountFlagResultModel outputResult = new AddLoanAccountFlagResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddLoanAccountFlagResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}