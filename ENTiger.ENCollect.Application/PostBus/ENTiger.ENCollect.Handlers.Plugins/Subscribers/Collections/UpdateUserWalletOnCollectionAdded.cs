using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// Handles updating the user wallet when a collection is added.
    /// </summary>
    public partial class UpdateUserWalletOnCollectionAdded : IUpdateUserWalletOnCollectionAdded
    {
        /// <summary>
        /// Logger instance for logging messages and errors.
        /// </summary>
        protected readonly ILogger<UpdateUserWalletOnCollectionAdded> _logger;

        /// <summary>
        /// Repository factory for accessing the database.
        /// </summary>
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        /// Condition that determines when the event should fire.
        /// </summary>
        protected string EventCondition = "";

        /// <summary>
        /// Context bridge for accessing the application context.
        /// </summary>
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserWalletOnCollectionAdded"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="repoFactory">The repository factory for database operations.</param>
        public UpdateUserWalletOnCollectionAdded(ILogger<UpdateUserWalletOnCollectionAdded> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Executes the logic for updating the user wallet when a collection is added.
        /// </summary>
        /// <param name="event">The event containing collection details.</param>
        /// <param name="serviceBusContext">The service bus context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            var repo = _repoFactory.GetRepo();
            var tenantId = _flexAppContext.TenantId;

            /// <summary>
            /// Retrieves the receipt posting configuration to check if wallet updates are enabled.
            /// </summary>
            var walletLimitEnabled = await repo.FindAll<FeatureMaster>()
                                           .FirstOrDefaultAsync(p => p.Parameter == "WalletLimitEnable");

            bool isWalletLimitEnabled = bool.TryParse(walletLimitEnabled?.Value, out var enabled) && enabled;

            if (!isWalletLimitEnabled)
            {
                _logger.LogWarning("Wallet limit update is disabled in the configuration.");
                return;
            }

            /// <summary>
            /// Retrieves the collection data based on the event's collection ID and cash collection mode.
            /// </summary>
            var collection = await repo.FindAll<Collection>()
                                 .FirstOrDefaultAsync(m => m.Id == @event.CollectionId && m.CollectionMode.Equals(CollectionModeEnum.Cash.Value));


            /// <summary>
            /// Retrieves the user who created the collection.
            /// </summary>
            var user = await repo.FindAll<ApplicationUser>()
                           .FirstOrDefaultAsync(u => u.Id == collection.CreatedBy);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", collection?.CreatedBy);
                return;
            }

            /// <summary>
            /// Updates the user's wallet details by adjusting the utilized wallet amount and updating the last updated date.
            /// </summary>
            //user.WalletLastUpdatedDate = DateTime.UtcNow;
            //user.UsedWallet += collection?.Amount ?? 0;

            repo.InsertOrUpdate(user);

            /// <summary>
            /// Saves all updates in a single transaction.
            /// </summary>
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("Updated user {UserId} for Collection {CollectionId} with amount {Amount}.",
                user?.Id, collection?.Id, collection?.Amount);
            }
            else
            {
                _logger.LogWarning("No records were updated in the database.");
            }

            /// <summary>
            /// Fires the event to notify other services of the wallet update.
            /// </summary>
            await this.Fire<UpdateUserWalletOnCollectionAdded>(EventCondition, serviceBusContext);
        }
    }
}
