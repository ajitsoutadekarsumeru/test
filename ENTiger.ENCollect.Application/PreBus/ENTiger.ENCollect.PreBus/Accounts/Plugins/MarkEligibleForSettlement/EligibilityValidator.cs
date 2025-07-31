using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule.MarkEligibleForSettlementAccountsPlugins
{
    public partial class EligibilityValidator : FlexiBusinessRuleBase, IFlexiBusinessRule<MarkEligibleForSettlementDataPacket>
    {
        public override string Id { get; set; } = "3a195056da950406a94a1f3f9448ce99";
        public override string FriendlyName { get; set; } = "EligibilityValidator";

        protected readonly ILogger<EligibilityValidator> _logger;
        protected readonly RepoFactory _repoFactory;

        public EligibilityValidator(ILogger<EligibilityValidator> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(MarkEligibleForSettlementDataPacket packet)
        {
            await Task.CompletedTask;
        }
    }
}
