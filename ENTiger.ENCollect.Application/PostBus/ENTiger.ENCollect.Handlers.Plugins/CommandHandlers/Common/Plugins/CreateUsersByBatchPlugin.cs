using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CreateUsersByBatchPlugin : FlexiPluginBase, IFlexiPlugin<CreateUsersByBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1582db77895965cd0c73e6008682cf";
        public override string FriendlyName { get; set; } = "CreateUsersByBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<CreateUsersByBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected UsersCreateFile? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public CreateUsersByBatchPlugin(ILogger<CreateUsersByBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CreateUsersByBatchPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<UsersCreateFile>().CreateUsersByBatch(packet.Cmd);

            _flexAppContext.RequestSource = TransactionSourceEnum.BulkUpload.Value;
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(UsersCreateFile).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(UsersCreateFile).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}