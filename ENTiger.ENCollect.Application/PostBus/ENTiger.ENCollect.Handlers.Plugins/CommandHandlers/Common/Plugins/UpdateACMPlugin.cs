using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateACMPlugin : FlexiPluginBase, IFlexiPlugin<UpdateACMPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139e481ea9b51cfe33516874aae4df";
        public override string FriendlyName { get; set; } = "UpdateACMPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateACMPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public UpdateACMPlugin(ILogger<UpdateACMPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(UpdateACMPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            string userId = packet.Cmd.Dto.GetAppContext()?.UserId;
            List<string> actionIds = packet.Cmd.Dto.Details.Select(x => x.Id).Distinct().ToList();

            _logger.LogInformation("UpdateACMFFPlugin : ActionIds - " + actionIds.Count);
            List<ActionMaster> actions = actions = await _repoFactory.GetRepo().FindAll<ActionMaster>().Where(x => actionIds.Contains(x.Id)).ToListAsync();

            _logger.LogInformation("UpdateACMFFPlugin : Actions Count - " + actions.Count);
            if (actions.Count > 0)
            {
                foreach (var action in actions)
                {
                    var model = packet.Cmd.Dto.Details.Where(x => x.Id == action.Id).FirstOrDefault();
                    if (model != null)
                    {
                        action.HasAccess = model.HasAccess;
                        action.SetLastModifiedBy(userId);
                        action.SetAddedOrModified();
                        _repoFactory.GetRepo().InsertOrUpdate(action);
                    }
                }
                int records = await _repoFactory.GetRepo().SaveAsync();
                _logger.LogInformation("UpdateACMFFPlugin : Saved Successfully");
            }
            _logger.LogInformation("UpdateACMFFPlugin : End");

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}