using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        protected FlexAppContextBridge? _flexAppContext;
        // protected GetPrimaryCategoryItemsParams _params;
        // Constructor to initialize _repoFactory

        public ProcessAccountsService(ILogger<ProcessAccountsService> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;

            _repoFactory = repoFactory;
        }

        public async Task<CommandResult> UpdateAccountContactDetails(UpdateAccountContactDetailsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateAccountContactDetailsDataPacket, UpdateAccountContactDetailsSequence, UpdateAccountContactDetailsDto, FlexAppContextBridge>(dto);

            _flexAppContext = dto.GetAppContext();
            _repoFactory.Init(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                var loanAccount = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => x.Id == dto.AccountId).FirstOrDefaultAsync();
                if (loanAccount != null)
                {
                    loanAccount.LatestMobileNo = dto.LatestMobileNo;
                    _repoFactory.GetRepo().InsertOrUpdate(loanAccount);
                    await _repoFactory.GetRepo().SaveAsync();
                }
                UpdateAccountContactDetailsCommand cmd = new UpdateAccountContactDetailsCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateAccountContactDetailsResultModel outputResult = new UpdateAccountContactDetailsResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateAccountContactDetailsResultModel : DtoBridge
    {
    }
}