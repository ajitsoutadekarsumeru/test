using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// Handles loan account updates when a Pay-In Slip is created.
    /// </summary>
    public partial class UpdateLoanAccountOnDepositSlipCreated : IUpdateLoanAccountOnDepositSlipCreated
    {
        protected readonly ILogger<UpdateLoanAccountOnDepositSlipCreated> _logger;
        protected readonly RepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        private readonly IPayInSlipRepository _payInSlipRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly ILoanAccountQueryRepository _accountRepository;

        private string _eventCondition = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateLoanAccountOnDepositSlipCreated"/> class.
        /// </summary>
        public UpdateLoanAccountOnDepositSlipCreated(
            ILogger<UpdateLoanAccountOnDepositSlipCreated> logger,
            RepoFactory repoFactory,
            IPayInSlipRepository payInSlipRepository,
            ICollectionRepository collectionRepository,
            ILoanAccountQueryRepository accountRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _payInSlipRepository = payInSlipRepository;
            _collectionRepository = collectionRepository;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Executes the event handler logic when a Pay-In Slip is created.
        /// </summary>
        /// <param name="event">The event payload.</param>
        /// <param name="serviceBusContext">The service bus context for event publishing.</param>
        public virtual async Task Execute(PayInSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; // Do not remove this line
            _repoFactory.Init(@event);

            _logger.LogInformation("Processing PayInSlipCreatedEvent with ID: {Id}", @event.Id);

            var payInSlip = await _payInSlipRepository.GetPayInSlipByIdAsync(@event.Id, _flexAppContext!);
            if (payInSlip?.CollectionBatches?.Any() != true)
            {
                _logger.LogWarning("No collection batches found for PayInSlip ID: {Id}", @event.Id);
                return;
            }

            var batchIds = payInSlip.CollectionBatches.Select(cb => cb.Id).ToList();
            var collections = await _collectionRepository.GetCollectionsByBatchIdsAsync(batchIds, _flexAppContext!);

            if (!collections.Any())
            {
                _logger.LogWarning("No collections found for batch IDs: {BatchIds}", string.Join(",", batchIds));
                return;
            }

            var loanAccounts = await GetLoanAccountsAsync(collections);

            UpdateLoanAccountsWithCollectionData(loanAccounts, collections);
            await SaveChangesAsync();

            // TODO: Assign _eventCondition based on business rules
            // _eventCondition = CONDITION_ONSUCCESS;

            await this.Fire<UpdateLoanAccountOnDepositSlipCreated>(_eventCondition, serviceBusContext);
        }

        /// <summary>
        /// Retrieves loan account details based on unique collection account IDs.
        /// </summary>
        /// <param name="collections">List of collections.</param>
        /// <returns>List of <see cref="LoanAccount"/> records.</returns>
        private async Task<List<LoanAccount>> GetLoanAccountsAsync(List<Collection> collections)
        {
            var accountIds = collections
                .Select(c => c.AccountId)
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .ToList();

            if (!accountIds.Any())
            {
                _logger.LogWarning("No valid account IDs found in collections.");
                return new();
            }

            var accounts = await _accountRepository.GetLoanAccountsByIdsAsync(accountIds, _flexAppContext!);
            if (accounts == null || !accounts.Any())
            {
                _logger.LogWarning("Loan accounts not found for account IDs: {AccountIds}", string.Join(",", accountIds));
                return new();
            }

            return accounts;
        }

        /// <summary>
        /// Updates loan account status using related collection data.
        /// </summary>
        /// <param name="accounts">List of loan accounts to update.</param>
        /// <param name="collections">List of collections associated with accounts.</param>
        private void UpdateLoanAccountsWithCollectionData(List<LoanAccount> accounts, List<Collection> collections)
        {
            foreach (var account in accounts)
            {
                var relatedCollection = collections.FirstOrDefault(c => c.AccountId == account.Id);

                account.Paid = true;
                account.Attempted = true; // Boolean flag - consider using Enum if multiple statuses
                account.LatestPaymentDate = relatedCollection.CreatedDate.DateTime;
                account.SetAddedOrModified();

                _repoFactory.GetRepo().InsertOrUpdate(account);
                _logger.LogInformation("Updated loan account ID: {AccountId}", account.Id);
            }
        }

        /// <summary>
        /// Persists all pending changes to the database.
        /// </summary>
        private async Task SaveChangesAsync()
        {
            int affectedRows = await _repoFactory.GetRepo().SaveAsync();
            _logger.LogInformation("Saved {Count} record(s) to loan accounts.", affectedRows);
        }
    }
}
