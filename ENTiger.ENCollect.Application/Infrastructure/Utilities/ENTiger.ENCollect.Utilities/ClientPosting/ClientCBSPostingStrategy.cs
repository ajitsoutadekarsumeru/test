using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Handles posting of client collection updates to the Core Banking System (CBS).
    /// </summary>
    public class ClientCBSPostingStrategy : IClientPostingStrategy
    {
        private readonly ILogger<ClientCBSPostingStrategy> _logger;

        private readonly ICollectionPoster _collectionPoster;
        private readonly ICollectionBatchPoster _collectionBatchPoster;
        private readonly IPayInSlipPoster _payInSlipPoster;

        /// <summary>
        /// Initializes a new instance of <see cref="ClientCBSPostingStrategy"/>.
        /// </summary>
        /// <param name="logger">Logger instance for logging information and errors.</param>
        public ClientCBSPostingStrategy(ILogger<ClientCBSPostingStrategy> logger, 
            ICollectionPoster collectionPoster,
            ICollectionBatchPoster collectionBatchPoster,
            IPayInSlipPoster payInSlipPoster)
        {
            _logger = logger ;
            _collectionPoster = collectionPoster;
            _collectionBatchPoster = collectionBatchPoster;
            _payInSlipPoster = payInSlipPoster;
        }

        /// <summary>
        /// Posts the collection receipt details to the client system.
        /// </summary>
        /// <param name="collection">Collection details to be posted.</param>
        /// <param name="paymentDetails">List of payment details containing configurations.</param>
        /// <param name="tenantId">Identifier for the tenant.</param>
        public async Task PostCollectionAsync(CollectionDtoWithId collection)
        {
            await _collectionPoster.PostCollectionAsync(collection);
        }


        public Task PostCollectBatchAsync(CollectionBatchDtoWithId collectionBatch, List<FeatureMasterDtoWithId> paymentDetails, string tenantId)
        {
            throw new NotImplementedException();
        }

        public async Task PostPayInSlipAsync(PayInSlipDtoWithId collectionBatch)
        {
            await _payInSlipPoster.PostPayInSlipAsync(collectionBatch);
        }
    }
}
