using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ProcessPublicService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ImportAccounts(ImportAccountsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ImportAccountsDataPacket, ImportAccountsSequence, ImportAccountsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ImportAccountsCommand cmd = new ImportAccountsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ImportAccountsResultModel outputResult = new ImportAccountsResultModel();
                //outputResult.Id = dto.GetGeneratedId();
                outputResult.TransactionNumber = dto.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ImportAccountsResultModel : DtoBridge
    {
        public string TransactionNumber { get; set; }
    }
}