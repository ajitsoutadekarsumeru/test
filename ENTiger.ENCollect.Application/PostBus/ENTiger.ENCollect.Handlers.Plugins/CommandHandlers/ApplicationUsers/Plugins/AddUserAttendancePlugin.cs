using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class AddUserAttendancePlugin : FlexiPluginBase, IFlexiPlugin<AddUserAttendancePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a132418b000ac8e30ed0f1416d1fb87";
        public override string FriendlyName { get; set; } = "AddUserAttendancePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddUserAttendancePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected UserAttendanceLog? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddUserAttendancePlugin(ILogger<AddUserAttendancePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddUserAttendancePostBusDataPacket packet)
        {
            try
            {
                _logger.LogInformation($"Push Notification to Token : {packet.Cmd.Dto.Token}");
                if (!string.IsNullOrEmpty(packet.Cmd.Dto.Token))
                {
                    NotificationRequest req = new NotificationRequest
                    {
                        Recipient = packet.Cmd.Dto.Token,
                        Title = "Hi",
                        Message = "Welcome to ENCollect Pro!"
                    };

                    var utility = _flexHost.GetUtilityService<PushNotificationProviderFactory>("");

                    var pushService = utility.GetPushNotificationProvider();

                    await pushService.SendNotificationAsync(req);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during push notification to token : {ex}");
            }
            //Commented flex gen code 
            //_flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            //_repoFactory.Init(packet.Cmd.Dto);

            //_model = _flexHost.GetDomainModel<UserAttendanceLog>().AddUserAttendance(packet.Cmd);

            //_repoFactory.GetRepo().InsertOrUpdate(_model);
            //int records = await _repoFactory.GetRepo().SaveAsync();
            //if (records > 0)
            //{
            //    _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(UserAttendanceLog).Name, _model.Id);
            //}
            //else
            //{
            //    _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(UserAttendanceLog).Name, _model.Id);
            //}

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            //await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}