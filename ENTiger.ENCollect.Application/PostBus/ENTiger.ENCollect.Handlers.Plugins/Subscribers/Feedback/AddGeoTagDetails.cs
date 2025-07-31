using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class AddGeoTagDetails : IAddGeoTagDetails
    {
        protected readonly ILogger<AddGeoTagDetails> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public AddGeoTagDetails(ILogger<AddGeoTagDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(FeedbackAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            _logger.LogInformation("AddGeoTagDetails : Start");
            GeoTagDetails geotag = await BuildAsync(@event.Id);
            if (geotag.Latitude != 0 && geotag.Longitude != 0)
            {
                geotag.TransactionType = "Trail";
                geotag.SetAdded();
                _repoFactory.GetRepo().InsertOrUpdate(geotag);
                int records = await _repoFactory.GetRepo().SaveAsync();
            }
            else
            {
                _logger.LogWarning("Error in AddGeoTagDetails : Latitude and Longitude values missing in Feedback");
            }

            _logger.LogInformation("AddGeoTagDetails : End");
            await this.Fire<AddGeoTagDetails>(EventCondition, serviceBusContext);
        }

        private async Task<GeoTagDetails> BuildAsync(string id)
        {
            _logger.LogInformation("AddGeoTagDetails : Build - Start");

            var feedback = await _repoFactory.GetRepo().FindAll<Feedback>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (feedback == null)
            {
                _logger.LogWarning("Feedback not found for Id: {Id}", id);
                return new GeoTagDetails();
            }

            var geotag = new GeoTagDetails
            {
                Latitude = feedback.Latitude != null ? Convert.ToDouble(feedback.Latitude) : 0,
                Longitude = feedback.Longitude != null ? Convert.ToDouble(feedback.Longitude) : 0,
                ApplicationUserId = feedback.CreatedBy,
                TransactionSource = feedback.TransactionSource
            };
            geotag.SetCreatedBy(feedback.CreatedBy);
            geotag.SetAccount(feedback.AccountId);
            _logger.LogInformation("AddGeoTagDetails : Build - End");
            return geotag;
        }
    }
}