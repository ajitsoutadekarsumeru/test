using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AllocateNewReceipt : IAllocateNewReceipt
    {
        protected readonly ILogger<AllocateNewReceipt> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;
        protected FlexAppContextBridge? _flexAppContext;

        public AllocateNewReceipt(ILogger<AllocateNewReceipt> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
        }

        public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            _logger.LogInformation("AllocateNewReceipt : Start");
            string loggedInUser = _flexAppContext.UserId;

            var eventmodel = @event;

            Receipt receipt = new Receipt();
            receipt.CollectorId = eventmodel.CollectorId;
            receipt.CustomId = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.EReceipt.Value);
            receipt.SetCreatedBy(eventmodel.CollectorId);
            receipt.SetLastModifiedBy(eventmodel.CollectorId);
            receipt.SetAdded();
            receipt.MarkAsAllocatedToCollector(loggedInUser);

            _logger.LogInformation("AllocateNewReceipt : User -  " + eventmodel.CollectorId + "| Receipt - " + receipt.CustomId);
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
            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of OnRaiseEventCondition according to your business logic
            //EventCondition = CONDITION_ONSUCCESS;

            _logger.LogInformation("AllocateNewReceipt : End");
            await Task.CompletedTask;
        }
    }
}