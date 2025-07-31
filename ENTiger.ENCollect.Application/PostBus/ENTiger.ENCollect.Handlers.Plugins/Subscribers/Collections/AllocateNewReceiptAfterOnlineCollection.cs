using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AllocateNewReceiptAfterOnlineCollection : IAllocateNewReceiptAfterOnlineCollection
    {
        protected readonly ILogger<AllocateNewReceiptAfterOnlineCollection> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ICustomUtility _customUtility;
        public AllocateNewReceiptAfterOnlineCollection(ILogger<AllocateNewReceiptAfterOnlineCollection> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
        }

        public virtual async Task Execute(OnlineCollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:
            var eventmodel = @event;

            Receipt receipt = new Receipt();
            receipt.CollectorId = eventmodel.CollectorId;
            receipt.CustomId = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.EReceipt.Value);
            receipt.SetCreatedBy(eventmodel.CollectorId);
            receipt.SetLastModifiedBy(eventmodel.CollectorId);

            receipt.SetAdded();
            receipt.MarkAsAllocatedToCollector(_flexAppContext?.UserId);
            _repoFactory.GetRepo().InsertOrUpdate(receipt);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database: ", typeof(Receipt).Name, receipt.CustomId);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Receipt).Name, receipt.CustomId);
            }
            _logger.LogInformation("AllocateNewReceipt Online: End");
        }
    }
}