using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Bcpg;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule.RequestSettlementSettlementPlugins
{
    public partial class UserBranchValidator : FlexiBusinessRuleBase, IFlexiBusinessRule<RequestSettlementDataPacket>
    {
        public override string Id { get; set; } = "c72874dd322811f0ac7a00ff9033f9ef";                                                       
        public override string FriendlyName { get; set; } = "UserBranchValidator";

        protected readonly ILogger<UserBranchValidator> _logger;
        protected readonly RepoFactory _repoFactory;
        protected readonly ILoanAccountQueryRepository _loanAccountRepository;
        protected readonly IApplicationUserQueryRepository _applicationUserQueryRepository;

        protected FlexAppContextBridge? _flexAppContext;

        public UserBranchValidator(ILogger<UserBranchValidator> logger, RepoFactory repoFactory
                , ILoanAccountQueryRepository loanAccountRepository, IApplicationUserQueryRepository applicationUserQueryRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _loanAccountRepository = loanAccountRepository;
            _applicationUserQueryRepository = applicationUserQueryRepository;
        }

        public virtual async Task Validate(RequestSettlementDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(_flexAppContext);

            List<string?>? userBranches = await _applicationUserQueryRepository.GetUserBranchByIdAsync(_flexAppContext, _flexAppContext.UserId);

            if (!userBranches.Contains("All", StringComparer.OrdinalIgnoreCase))
            {
                string? loanAccountBranch = await _loanAccountRepository.GetLoanAccountBranch(packet.Dto.LoanAccountId, _flexAppContext);

                if (!userBranches.Any(b => b.Equals(loanAccountBranch, StringComparison.OrdinalIgnoreCase)))
                {
                    packet.AddError("Error", "The selected account does not belong to the user's assigned branch");
                }
            }
        }
    }
}
