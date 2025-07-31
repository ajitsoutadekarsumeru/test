using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class UpdateUserGeoLocation : IUpdateUserGeoLocation
    {
        protected readonly ILogger<UpdateUserGeoLocation> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateUserGeoLocation(ILogger<UpdateUserGeoLocation> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UserGeoLocationUpdateRequested @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            _logger.LogInformation("AddGeoTagDetails : Start");
            UserAttendanceLog userAttendanceLog = await BuildAsync(@event.UserAttendanceId);
            if (userAttendanceLog != null)
            {
                userAttendanceLog.GeoLocation = @event.GeoLocation;
                userAttendanceLog.SetAddedOrModified();
                _repoFactory.GetRepo().InsertOrUpdate(userAttendanceLog);
                await _repoFactory.GetRepo().SaveAsync();
            }

            await Task.CompletedTask;
        }

        private async Task<UserAttendanceLog> BuildAsync(string id)
        {
            _logger.LogInformation("UserAttendanceLog : Build - Start");

            var userAttendanceLog = await _repoFactory.GetRepo().FindAll<UserAttendanceLog>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (userAttendanceLog == null)
            {
                _logger.LogWarning("UserAttendanceLog not found for Id: {Id}" + id);
                return new UserAttendanceLog();
            }
            _logger.LogInformation("UserAttendanceLog : Build - End");
            return userAttendanceLog;
        }
    }
}
