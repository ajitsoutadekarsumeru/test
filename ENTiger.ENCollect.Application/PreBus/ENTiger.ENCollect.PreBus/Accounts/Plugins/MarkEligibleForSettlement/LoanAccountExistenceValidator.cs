using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule.MarkEligibleForSettlementAccountsPlugins
{
    public partial class LoanAccountExistenceValidator : FlexiBusinessRuleBase, IFlexiBusinessRule<MarkEligibleForSettlementDataPacket>
    {
        public override string Id { get; set; } = "3a195056df74720e538c7b28911c946d";
        public override string FriendlyName { get; set; } = "LoanAccountExistenceValidator";

        protected readonly ILogger<LoanAccountExistenceValidator> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ILoanAccountQueryRepository _accountRepository;

        public LoanAccountExistenceValidator(ILogger<LoanAccountExistenceValidator> logger, 
            IRepoFactory repoFactory,
            ILoanAccountQueryRepository accountRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// validates the loan account IDs existence in the database.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public virtual async Task Validate(MarkEligibleForSettlementDataPacket packet)
        {
            List<string> ids = packet.Dto.LoanAccountIds;

            _repoFactory.Init(packet.Dto);

            int existingAccountCount = await _accountRepository.GetValidAccountCount(ids,packet.Dto.GetAppContext());
            int invalidCount = ids.Count - existingAccountCount;

            if (invalidCount > 0)
            {
                packet.AddError("Error", $"{invalidCount} loan account(s) are invalid.");
            }
        }
    }
}
