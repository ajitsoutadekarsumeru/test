using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountLabelsPlugin : FlexiPluginBase, IFlexiPlugin<UpdateAccountLabelsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13ebed3f7cb9a808f7e4ed3d7d4e54";
        public override string FriendlyName { get; set; } = "UpdateAccountLabelsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateAccountLabelsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<AccountLabels>? _model;        
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateAccountLabelsPlugin(ILogger<UpdateAccountLabelsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateAccountLabelsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            string userId = _flexAppContext.UserId;

            List<string> Ids = new List<string>();
            Ids = packet.Cmd.Dto.Labels.Select(x => x.Id).Distinct().ToList();
            _model = await _repoFactory.GetRepo().FindAll<AccountLabels>().Where(m => Ids.Contains(m.Id)).ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    var itemToChange = packet.Cmd.Dto.Labels.FirstOrDefault(d => d.Id == obj.Id);
                    if (itemToChange != null)
                    {
                        obj.Name = itemToChange.Name;
                        obj.SetLastModifiedBy(userId);
                        obj.SetLastModifiedDate(DateTime.UtcNow);
                        obj.SetAddedOrModified();
                        _repoFactory.GetRepo().InsertOrUpdate(obj);
                    }
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(AccountLabels).Name, _model.ToString());
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(AccountLabels).Name, _model.ToString());
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(AccountLabels).Name, packet.Cmd.Dto.Labels.ToString());

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }
            //await this.Fire(EventCondition, packet.FlexServiceBusContext);
            await Task.CompletedTask;
        }
    }
}