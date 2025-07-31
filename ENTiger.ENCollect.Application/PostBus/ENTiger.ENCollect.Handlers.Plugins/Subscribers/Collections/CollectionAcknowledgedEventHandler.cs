using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionAcknowledgedEventHandler : ICollectionAcknowledgedEventHandler
    {
        private readonly ILogger<CollectionAcknowledgedEventHandler> _logger;
        private readonly IRepoFactory _repoFactory;
        private readonly WalletSettings _walletSettings;
        private readonly IWalletRepository _walletRepository;
        private FlexAppContextBridge? _flexAppContext;
        private const string EventCondition = ""; // Event condition

        public CollectionAcknowledgedEventHandler(
            ILogger<CollectionAcknowledgedEventHandler> logger,
            IRepoFactory repoFactory,
            IOptions<WalletSettings> walletSettings,
            IWalletRepository walletRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _walletSettings = walletSettings.Value;
            _walletRepository = walletRepository;   
        }

        public virtual async Task Execute(CollectionAcknowledgedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; // Do not remove this line
            _repoFactory.Init(@event);

            /// <summary>
            /// Retrieves the receipt posting configuration to check if wallet updates are enabled.
            /// </summary>
            var walletLimitEnabled = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                           .FirstOrDefaultAsync(p => p.Parameter == "WalletLimitEnable");

            bool isWalletLimitEnabled = bool.TryParse(walletLimitEnabled?.Value, out var enabled) && enabled;

            if (!isWalletLimitEnabled)
            {
                _logger.LogWarning("Wallet limit update is disabled in the configuration.");
                return;
            }

            // Convert CollectionIds to HashSet for optimized lookup
            var collectionIds = @event.CollectionIds?.ToHashSet() ?? new HashSet<string>();

            // Fetch only relevant collections
            var collections = await _repoFactory.GetRepo()
                            .FindAll<Collection>()
                            .Select(a=> new { 
                                a.Id,
                                a.CustomId,
                                a.CollectionMode,
                                a.Amount,
                                a.Collector.Wallet
                            })
                            .Where(m => collectionIds.Contains(m.Id) 
                            &&
                            m.CollectionMode.Equals(CollectionModeEnum.Cash.Value))
                                .ToListAsync();

           
            // Update users' wallet data
            foreach (var collection in collections)
            {
                decimal amount = (decimal)collection.Amount;
                Wallet? wallet = collection.Wallet;
                if (wallet!=null)
                {
                    wallet.ReleaseFunds(amount);
                    _repoFactory.GetRepo().InsertOrUpdate(wallet);
                    _logger.LogInformation("Wallet :: released funds" + amount + "for collector " + collection.CustomId);
                }
            }

            // Save all updates in a single transaction
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} updates committed successfully in the database.", nameof(Collection));
            }
            else
            {
                _logger.LogWarning("No records were updated in the database.");
            }

            await this.Fire<CollectionAcknowledgedEventHandler>(EventCondition, serviceBusContext);
        }
    }
}
