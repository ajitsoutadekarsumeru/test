using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// Plugin to update payment status for an online collection.
    /// </summary>
    public partial class UpdatePaymentStatusPlugin : FlexiPluginBase, IFlexiPlugin<UpdatePaymentStatusPostBusDataPacket>
    {
        private readonly ILogger<UpdatePaymentStatusPlugin> _logger;
        private readonly IRepoFactory _repoFactory;
        private readonly ICollectionRepository _collectionRepository;
        private FlexAppContextBridge? _flexAppContext;

        protected string EventCondition = "";

        public override string Id { get; set; } = "3a138e3c26f7a945a74eee26f7d0a1bf";
        public override string FriendlyName { get; set; } = "UpdatePaymentStatusPlugin";

        /// <summary>
        /// Initializes a new instance of <see cref="UpdatePaymentStatusPlugin"/>.
        /// </summary>
        public UpdatePaymentStatusPlugin(
            IRepoFactory repoFactory,
            ICollectionRepository collectionRepository,
            ILogger<UpdatePaymentStatusPlugin> logger)
        {
            _repoFactory = repoFactory ?? throw new ArgumentNullException(nameof(repoFactory));
            _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Executes the plugin logic to update payment status based on collection ID.
        /// </summary>
        public virtual async Task Execute(UpdatePaymentStatusPostBusDataPacket packet)
        {
            if (packet?.Cmd?.Dto == null)
            {
                _logger.LogWarning("UpdatePaymentStatusPlugin: Invalid packet or DTO received.");
                return;
            }

            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);

            var collectionId = packet.Cmd.Dto.CollectionId;
            var repo = _repoFactory.GetRepo();

            var collectionDto = await repo
                .FindAll<Collection>()
                .ByCollectionId(collectionId)
                .FlexInclude(c => c.Account)
                 .SelectTo<CollectionDtoWithId>()
                .FirstOrDefaultAsync();
            if (collectionDto == null)
            {
                _logger.LogWarning("UpdatePaymentStatusPlugin: No collection found with ID: {CollectionId}", collectionId);
               // EventCondition = CONDITION_ONFAILURE;
            }
            else
            {
                collectionDto.SetAppContext(_flexAppContext!);
                var transaction = await GetPaymentTransactionAsync(collectionDto.CustomId);
                _logger.LogInformation("UpdatePaymentStatusPlugin: Found collection with ID: {CollectionId}.", collectionId);
                // TODO: Add logic to update payment status, send notifications, etc.

                EventCondition = CONDITION_ONSUCCESS;
            }

            await Fire(EventCondition, packet.FlexServiceBusContext);

        }
        private async Task<PaymentTransaction?> GetPaymentTransactionAsync(string receiptNumber)
        {
            return await _repoFactory.GetRepo()
                .FindAll<PaymentTransaction>().FlexInclude(a => a.PaymentGateway)
                .FirstOrDefaultAsync(t => t.MerchantTransactionId == receiptNumber);
        }
    }

}
