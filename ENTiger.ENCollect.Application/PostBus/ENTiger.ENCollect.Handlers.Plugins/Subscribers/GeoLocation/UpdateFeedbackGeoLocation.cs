using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class UpdateFeedbackGeoLocation : IUpdateFeedbackGeoLocation
    {
        protected readonly ILogger<UpdateFeedbackGeoLocation> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateFeedbackGeoLocation(ILogger<UpdateFeedbackGeoLocation> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(FeedbackGeoLocationUpdateRequested @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            _logger.LogInformation("AddGeoTagDetails : Start");
            Feedback feedback = await BuildAsync(@event.FeedbackId);
            if (feedback != null)
            {
                feedback.GeoLocation = @event.GeoLocation;
                feedback.SetAddedOrModified();
                _repoFactory.GetRepo().InsertOrUpdate(feedback);
                await _repoFactory.GetRepo().SaveAsync();
            }
        }
        private async Task<Feedback> BuildAsync(string id)
        {
            _logger.LogInformation("Feedback : Build - Start");

            var feedback = await _repoFactory.GetRepo().FindAll<Feedback>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (feedback == null)
            {
                _logger.LogWarning("Feedback not found for Id: {Id}" + id);
                 return new Feedback();
            }
            _logger.LogInformation("Feedback : Build - End");
            return feedback;
        }
    }
}
