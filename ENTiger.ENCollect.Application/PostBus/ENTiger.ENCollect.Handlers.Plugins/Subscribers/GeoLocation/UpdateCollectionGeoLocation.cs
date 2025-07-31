using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class UpdateCollectionGeoLocation : IUpdateCollectionGeoLocation
    {
        protected readonly ILogger<UpdateCollectionGeoLocation> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateCollectionGeoLocation(ILogger<UpdateCollectionGeoLocation> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionGeoLocationUpdateRequested @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            _logger.LogInformation("AddGeoTagDetails : Start");
            Collection collection = await BuildAsync(@event.CollectionId);
            if (collection != null)
            {
                collection.GeoLocation = @event.GeoLocation;
                collection.SetAddedOrModified();
                _repoFactory.GetRepo().InsertOrUpdate(collection);
                await _repoFactory.GetRepo().SaveAsync();
            }

            await Task.CompletedTask;
        }

        private async Task<Collection> BuildAsync(string id)
        {
            _logger.LogInformation("collection : Build - Start");

            var collection = await _repoFactory.GetRepo().FindAll<Collection>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (collection == null)
            {
                _logger.LogWarning("Collection not found for Id: {Id}"+ id);
            }            
            _logger.LogInformation("collection : Build - End");
            return collection;
        }
    }
}
