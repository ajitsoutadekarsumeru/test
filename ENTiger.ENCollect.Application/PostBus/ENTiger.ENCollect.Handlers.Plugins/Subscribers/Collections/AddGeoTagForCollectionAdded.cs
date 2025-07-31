using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddGeoTagForCollectionAdded : IAddGeoTagForCollectionAdded
    {
        protected readonly ILogger<AddGeoTagForCollectionAdded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public AddGeoTagForCollectionAdded(ILogger<AddGeoTagForCollectionAdded> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("AddGeoTagForCollectionAdded : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            var eventModel = @event;
            GeoTagDetails geotag = await BuildAsync(eventModel.Id);

            if (geotag.Latitude != 0 && geotag.Longitude != 0)
            {
                geotag.TransactionType = "Receipt";
                geotag.SetAdded();
                _repoFactory.GetRepo().InsertOrUpdate(geotag);
                int records = await _repoFactory.GetRepo().SaveAsync();
            }
            else
            {
                _logger.LogWarning("AddGeoTagForCollectionAdded : Latitude and Longitude values missing in collection");
            }
            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of OnRaiseEventCondition according to your business logic
            //EventCondition = CONDITION_ONSUCCESS;
            //await this.Fire<AddGeoTagForCollectionAdded>(EventCondition, serviceBusContext);
            _logger.LogInformation("AddGeoTagForCollectionAdded : End");
            await Task.CompletedTask;
        }

        private async Task<GeoTagDetails> BuildAsync(string id)
        {
            _logger.LogInformation("AddGeoTagForCollectionAdded : Build - Start");

            var collection = await _repoFactory.GetRepo().FindAll<Collection>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (collection == null)
            {
                _logger.LogWarning("Collection not found for Id: {Id}", id);
                return new GeoTagDetails();
            }

            var geotag = new GeoTagDetails
            {
                Latitude = !string.IsNullOrEmpty(collection.Latitude) ? Convert.ToDouble(collection.Latitude) : 0,
                Longitude = !string.IsNullOrEmpty(collection.Longitude) ? Convert.ToDouble(collection.Longitude) : 0,
                ApplicationUserId = collection.CollectorId,
                TransactionSource = collection.TransactionSource
            };
            geotag.SetCreatedBy(collection.CollectorId);
            geotag.SetAccount(collection.AccountId);
            _logger.LogInformation("AddGeoTagForCollectionAdded : Build - End");
            return geotag;
        }
    }
}