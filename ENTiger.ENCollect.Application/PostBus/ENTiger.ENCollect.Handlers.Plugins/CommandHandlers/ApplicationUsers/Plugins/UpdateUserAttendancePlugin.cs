using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class UpdateUserAttendancePlugin : FlexiPluginBase, IFlexiPlugin<UpdateUserAttendancePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1324198d0f4f7fa03f39b9beea6826";
        public override string FriendlyName { get; set; } = "UpdateUserAttendancePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateUserAttendancePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected UserAttendanceLog? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateUserAttendancePlugin(ILogger<UpdateUserAttendancePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateUserAttendancePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<UserAttendanceLog>().FirstOrDefaultAsync();

            if (_model != null)
            {
                _model.UpdateUserAttendance(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(UserAttendanceLog).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(UserAttendanceLog).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(UserAttendanceLog).Name, packet.Cmd.Dto.ToString());

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}