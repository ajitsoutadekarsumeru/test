using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// Handles updating the loan account when a collection is acknowledged.
    /// </summary>
    public partial class UpdateLoanAccountOnCollectionAcknowledged : IUpdateLoanAccountOnCollectionAcknowledged
    {
        private readonly ILogger<UpdateLoanAccountOnCollectionAcknowledged> _logger;
        private readonly RepoFactory _repoFactory;
        private readonly ILoanAccountQueryRepository _accountRepository;
        private readonly ICollectionRepository _collectionRepository;

        private FlexAppContextBridge? _flexAppContext;
        private string _eventCondition = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateLoanAccountOnCollectionAcknowledged"/> class.
        /// </summary>
        public UpdateLoanAccountOnCollectionAcknowledged(
            ILogger<UpdateLoanAccountOnCollectionAcknowledged> logger,
            RepoFactory repoFactory,
            ILoanAccountQueryRepository accountRepository,
            ICollectionRepository collectionRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _accountRepository = accountRepository;
            _collectionRepository = collectionRepository;
        }

        /// <summary>
        /// Executes logic to update loan account when a collection is acknowledged.
        /// </summary>
        /// <param name="event">The collection acknowledged event containing context and collection IDs.</param>
        /// <param name="serviceBusContext">The service bus context used for further event firing.</param>
        public virtual async Task Execute(CollectionAcknowledgedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("UpdateLoanAccountOnCollectionAcknowledged Execute: Start");

            _flexAppContext = @event.AppContext;

            var repo = _repoFactory.Init(@event);

            var collections = await GetCollectionsAsync(@event.CollectionIds);
            if (!collections.Any()) return;

            var loanAccounts = await GetLoanAccountsAsync(collections);

            UpdateLoanAccountsWithCollectionData(loanAccounts, collections);

            await SaveChangesAsync();

            await FireEventAsync(serviceBusContext);

        }

        /// <summary>
        /// Retrieves collection records by their IDs.
        /// </summary>
        /// <param name="collectionIds">List of collection IDs to fetch.</param>
        /// <returns>A list of matching <see cref="Collection"/> records.</returns>
        private async Task<List<Collection>> GetCollectionsAsync(List<string> collectionIds)
        {
            var collections = await _collectionRepository.GetCollectionsByIdsAsync(collectionIds, _flexAppContext!);
            if (collections == null || !collections.Any())
            {
                _logger.LogWarning("No collections found for the provided CollectionIds.");
                return new List<Collection>();
            }

            return collections;
        }

        /// <summary>
        /// Retrieves loan account details based on collection account IDs.
        /// </summary>
        /// <param name="collections">The list of collections used to extract account IDs.</param>
        /// <returns>A list of matching <see cref="LoanAccount"/> records.</returns>
        private async Task<List<LoanAccount>> GetLoanAccountsAsync(List<Collection> collections)
        {
            var accountIds = collections.Select(x => x.AccountId).Distinct().ToList();
            var accounts = await _accountRepository.GetLoanAccountsByIdsAsync(accountIds, _flexAppContext!);
            if (accounts == null || !accounts.Any())
            {
                _logger.LogWarning("No loan accounts found for the corresponding collection account IDs.");
                return new List<LoanAccount>();
            }

            return accounts;
        }

        /// <summary>
        /// Updates loan account properties based on collection data.
        /// </summary>
        /// <param name="accounts">List of loan accounts to update.</param>
        /// <param name="collections">List of related collections.</param>
        private void UpdateLoanAccountsWithCollectionData(List<LoanAccount> accounts, List<Collection> collections)
        {
            foreach (var account in accounts)
            {
                var relatedCollection = collections.FirstOrDefault(c => c.AccountId == account.Id);
                if (relatedCollection == null) continue;

                account.Paid = true;
                account.Attempted = true;
                account.LatestPaymentDate = relatedCollection.CreatedDate.DateTime;
                account.SetAddedOrModified();

                _repoFactory.GetRepo().InsertOrUpdate(account);
            }
        }

        /// <summary>
        /// Saves all pending changes to the database.
        /// </summary>
        private async Task SaveChangesAsync()
        {
            int affected = await _repoFactory.GetRepo().SaveAsync();
            _logger.LogInformation($"Saved {affected} record(s) to loan accounts.");
        }

        /// <summary>
        /// Fires the follow-up event in the workflow.
        /// </summary>
        /// <param name="context">The Flex service bus context used for firing events.</param>
        private async Task FireEventAsync(IFlexServiceBusContext context)
        {
            await this.Fire<UpdateLoanAccountOnCollectionAcknowledged>(_eventCondition, context);
        }
    }
}
