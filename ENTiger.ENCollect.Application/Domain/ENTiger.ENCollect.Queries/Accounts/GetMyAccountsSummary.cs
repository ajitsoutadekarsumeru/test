using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// Responsible for retrieving the account summary for the currently logged-in user.
    /// </summary>
    public class GetMyAccountsSummary : FlexiQueryBridgeAsync<LoanAccount, GetMyAccountsSummaryDto>
    {
        private readonly ILogger<GetMyAccountsSummary> _logger;
        private readonly RepoFactory _repoFactory;
        private readonly ILoanAccountQueryRepository _accountRepository;

        private GetMyAccountsSummaryParams _params;
        private FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyAccountsSummary"/> class.
        /// </summary>
        /// <param name="logger">Logger for logging information and errors.</param>
        /// <param name="repoFactory">Repository factory to access data repositories.</param>
        /// <param name="accountRepository">Repository to fetch account projections.</param>
        public GetMyAccountsSummary(
            ILogger<GetMyAccountsSummary> logger,
            RepoFactory repoFactory,
            ILoanAccountQueryRepository accountRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Assigns the necessary parameters and application context.
        /// </summary>
        /// <param name="params">Parameters for executing the query.</param>
        /// <returns>Returns the current instance with parameters assigned.</returns>
        public GetMyAccountsSummary AssignParameters(GetMyAccountsSummaryParams @params)
        {
            _params = @params;
            _flexAppContext = _params.GetAppContext();
            return this;
        }

        /// <summary>
        /// Fetches the account summary data for the current user.
        /// </summary>
        /// <returns>A DTO containing summarized account metrics.</returns>
        public override async Task<GetMyAccountsSummaryDto> Fetch()
        {
            _repoFactory.Init(_params);

            var accounts = await Build<LoanAccount>().ToListAsync();
            if (accounts is not { Count: > 0 })
                return new();

            var accountIds = accounts.Select(x => x.Id).ToList();
            var projections = accounts.SelectMany(x => x.Projections.Where(z => z.Month == DateTime.Now.Month && z.Year == DateTime.Now.Year)).ToList();
            var totalStats = CalculateTotalStats(accounts);
            var collectedStats = CalculateStats(projections, p => (p.TotalCollectionCount ?? 0) > 0);
            var attemptedStats = CalculateStats(projections, p => (p.TotalTrailCount ?? 0) > 0);

            var dispositionGroups = GetDispositionGroups(projections.Where(p => (p.TotalTrailCount ?? 0) > 0));

            var ptpStats = GetStats(dispositionGroups, DispositionGroupEnum.PTP.Value);
            var contactedStats = GetStats(dispositionGroups, DispositionGroupEnum.Contacted.Value);
            var noContactStats = GetStats(dispositionGroups, DispositionGroupEnum.NoContact.Value);

            var otherStats = SubtractStats(attemptedStats, ptpStats, contactedStats, noContactStats);
            var unAttemptedStats = SubtractStats(totalStats, attemptedStats, collectedStats);
            var paymentStatusGroups = accounts
                 .GroupBy(a => a.PAYMENTSTATUS ?? "Unknown")
                 .ToDictionary(
                   g => g.Key,
                 g => new SummaryStats
                 {
                     Count = g.Count(),
                     POS = g.Sum(x => x.CURRENT_POS ?? 0),
                     TAD = g.Sum(x => x.CURRENT_TOTAL_AMOUNT_DUE ?? 0)
                 });

            var rollBackStats = GetStats(paymentStatusGroups, PaymentStatusEnum.RB.Value);
            var stabilizedStats = GetStats(paymentStatusGroups, PaymentStatusEnum.Stab.Value);
            var normalizedStats = GetStats(paymentStatusGroups, PaymentStatusEnum.Norm.Value);
            var rollForwardStats = GetStats(paymentStatusGroups, PaymentStatusEnum.RF.Value);
            var totalBPTPCount = projections.Sum(p => p.TotalBPTPCount ?? 0);
            var totalTrailsCount = await _repoFactory.GetRepo().FindAll<Feedback>()
                                                     .ByFeedbackToday()
                                                     .ByAccountIds(accountIds).CountAsync();
            return new GetMyAccountsSummaryDto
            {
                TotalTrailsCount = totalTrailsCount,
                TotalAllocationCount = totalStats.Count,
                TotalAllocationPOS = totalStats.POS,
                TotalAllocationTAD = totalStats.TAD,

                CollectedCount = collectedStats.Count,
                CollectedPOS = collectedStats.POS,
                CollectedTAD = collectedStats.TAD,

                AttemptedCount = attemptedStats.Count,
                AttemptedPOS = attemptedStats.POS,
                AttemptedTAD = attemptedStats.TAD,

                UnAttemptedCount = unAttemptedStats.Count,
                UnAttemptedPOS = unAttemptedStats.POS,
                UnAttemptedTAD = unAttemptedStats.TAD,

                Attempted_PTP_Count = ptpStats.Count,
                Attempted_PTP_POS = ptpStats.POS,
                Attempted_PTP_TAD = ptpStats.TAD,

                Attempted_Contacted_Count = contactedStats.Count,
                Attempted_Contacted_POS = contactedStats.POS,
                Attempted_Contacted_TAD = contactedStats.TAD,

                Attempted_NoContact_Count = noContactStats.Count,
                Attempted_NoContact_POS = noContactStats.POS,
                Attempted_NoContact_TAD = noContactStats.TAD,

                Attempted_Others_Count = otherStats.Count,
                Attempted_Others_POS = otherStats.POS,
                Attempted_Others_TAD = otherStats.TAD,
                RollBackCount = rollBackStats.Count,
                RollBackPOS = rollBackStats.POS,
                RollBackTAD = rollBackStats.TAD,

                StabilizedCount = stabilizedStats.Count,
                StabilizedPOS = stabilizedStats.POS,
                StabilizedTAD = stabilizedStats.TAD,

                NormalizedCount = normalizedStats.Count,
                NormalizedPOS = normalizedStats.POS,
                NormalizedTAD = normalizedStats.TAD,

                RollForwardCount = rollForwardStats.Count,
                RollForwardPOS = rollForwardStats.POS,
                RollForwardTAD = rollForwardStats.TAD,
                TotalBrokenPTPCount = totalBPTPCount,
            };
        }

        /// <summary>
        /// Builds the base query for a given entity type and filters by the current user.
        /// </summary>
        /// <typeparam name="T">The entity type to query.</typeparam>
        /// <returns>An <see cref="IQueryable{T}"/> filtered by collector ID.</returns>
        protected override IQueryable<T> Build<T>()
        {
            string userId = _flexAppContext.UserId;
            return _repoFactory.GetRepo().FindAll<T>().ByCollectorId(userId).Include(x => x.Projections);
        }

        /// <summary>
        /// Calculates the total count, POS, and TAD from a list of accounts.
        /// </summary>
        /// <param name="accounts">The list of loan accounts.</param>
        /// <returns>Summary statistics for the given accounts.</returns>
        private SummaryStats CalculateTotalStats(List<LoanAccount> accounts) => new()
        {
            Count = accounts.Count,
            POS = accounts.Sum(x => x.CURRENT_POS ?? 0),
            TAD = accounts.Sum(x => x.CURRENT_TOTAL_AMOUNT_DUE ?? 0)
        };

        /// <summary>
        /// Calculates summary statistics based on a projection filter.
        /// </summary>
        /// <param name="projections">The list of loan account projections.</param>
        /// <param name="filter">The condition to filter projections.</param>
        /// <returns>Filtered summary statistics.</returns>
        private SummaryStats CalculateStats(IEnumerable<LoanAccountsProjection> projections, Func<LoanAccountsProjection, bool> filter)
        {
            var filtered = projections.Where(filter).ToList();
            return new SummaryStats
            {
                Count = filtered.Count,
                POS = filtered.Sum(x => x.LoanAccount?.CURRENT_POS ?? 0),
                TAD = filtered.Sum(x => x.LoanAccount?.CURRENT_TOTAL_AMOUNT_DUE ?? 0)
            };
        }

        /// <summary>
        /// Groups projections by disposition group and calculates stats for each.
        /// </summary>
        /// <param name="attempted">The attempted projections.</param>
        /// <returns>A dictionary mapping group names to their respective summary stats.</returns>
        private Dictionary<string, SummaryStats> GetDispositionGroups(IEnumerable<LoanAccountsProjection> attempted)
        {
            return attempted
                .GroupBy(x => x.CurrentDispositionGroup ?? "Unknown")
                .ToDictionary(
                    g => g.Key,
                    g => new SummaryStats
                    {
                        Count = g.Count(),
                        POS = g.Sum(x => x.LoanAccount?.CURRENT_POS ?? 0),
                        TAD = g.Sum(x => x.LoanAccount?.CURRENT_TOTAL_AMOUNT_DUE ?? 0)
                    });
        }

        /// <summary>
        /// Retrieves summary stats for a specific disposition group.
        /// </summary>
        /// <param name="groups">The grouped disposition statistics.</param>
        /// <param name="key">The group key to retrieve stats for.</param>
        /// <returns>The corresponding <see cref="SummaryStats"/> or empty if not found.</returns>
        private SummaryStats GetStats(Dictionary<string, SummaryStats> groups, string key) =>
            groups.TryGetValue(key, out var stats) ? stats : new SummaryStats();

        /// <summary>
        /// Subtracts multiple sets of stats from a base summary.
        /// </summary>
        /// <param name="baseStats">The base statistics.</param>
        /// <param name="toSubtract">One or more stats to subtract.</param>
        /// <returns>The resulting <see cref="SummaryStats"/> after subtraction.</returns>
        private SummaryStats SubtractStats(SummaryStats baseStats, params SummaryStats[] toSubtract)
        {
            return new SummaryStats
            {
                Count = baseStats.Count - toSubtract.Sum(x => x.Count),
                POS = baseStats.POS - toSubtract.Sum(x => x.POS),
                TAD = baseStats.TAD - toSubtract.Sum(x => x.TAD)
            };
        }
    }

    /// <summary>
    /// Parameter class for MyAccountsSummary query.
    /// </summary>
    public class GetMyAccountsSummaryParams : DtoBridge
    {
    }
}
