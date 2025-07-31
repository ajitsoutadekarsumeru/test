using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AccountImport(AccountImportDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AccountImportDataPacket, AccountImportSequence, AccountImportDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                dto.customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                AccountImportCommand cmd = new AccountImportCommand
                {
                    Dto = dto,
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AccountImportResultModel outputResult = new AccountImportResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = dto.customid;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AccountImportResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
    }
}