using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class UpdateToClientForCollectionAdded : IUpdateToClientForCollectionAdded
    {
        private readonly ILogger<UpdateToClientForCollectionAdded> _logger;
        private readonly IRepoFactory _repositoryFactory;
        private readonly IFlexHost _flexHost;
        private FlexAppContextBridge? _appContext;
        private CollectionDtoWithId? _collectionDetails;

        public UpdateToClientForCollectionAdded(
            ILogger<UpdateToClientForCollectionAdded> logger,
            IRepoFactory repositoryFactory,
            IFlexHost flexHost)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _flexHost = flexHost;
        }

        public virtual async Task Execute(CollectionAddedEvent eventData, IFlexServiceBusContext serviceBusContext)
        {
            _appContext = eventData.AppContext;
            var repo = _repositoryFactory.Init(eventData);
            var tenantId = _appContext.TenantId;

            _logger.LogInformation("Processing CollectionAddedEvent | CollectionId: {CollectionId} | Tenant: {TenantId}",
                eventData.CollectionId, tenantId);

            try
            {
                var receiptPostingEnableConfig = await repo.GetRepo().FindAll<FeatureMaster>().FirstOrDefaultAsync(p => p.Parameter == "ReceiptPostingEnable");


                bool isReceiptPostingEnabled = bool.TryParse(receiptPostingEnableConfig?.Value, out var enabled) && enabled;

                if (!isReceiptPostingEnabled)
                {
                    _logger.LogWarning("Receipt CBS posting is disabled for Tenant: {TenantId}", tenantId);
                    return;
                }

                _collectionDetails = await repo.GetRepo()
                                         .FindAll<Collection>()
                                         .ByCollectionId(eventData.Id)
                                         .SelectTo<CollectionDtoWithId>()
                                         .SingleOrDefaultAsync();


                if (_collectionDetails.CollectionMode?.Equals("CASH", StringComparison.OrdinalIgnoreCase) == true)
                {
                    _logger.LogInformation("Skipping receipt CBS posting for Tenant: {TenantId} due to collection mode: {Mode}",
                        tenantId, _collectionDetails.CollectionMode);
                    return;
                }

                var paymentPostingFactory = _flexHost.GetUtilityService<ClientPostingStrategyFactory>();
                var postingStrategy = paymentPostingFactory.GetStrategy();
                _collectionDetails.SetAppContext(_appContext);

                await postingStrategy.PostCollectionAsync(_collectionDetails);

                _logger.LogInformation("CollectionAddedEvent processed successfully | CollectionId: {CollectionId} | Tenant: {TenantId}",
                         eventData.Id, tenantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing CollectionAddedEvent | CollectionId: {CollectionId} | Tenant: {TenantId}",
                   eventData.Id, tenantId);
            }
        }
    }
}
