using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersActivatePlugin : FlexiPluginBase, IFlexiPlugin<AgencyUsersActivatePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19940cf6650aa81634b64313a5f434";
        public override string FriendlyName { get; set; } = "AgencyUsersActivatePlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<AgencyUsersActivatePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<AgencyUser>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<AgencyUser>? _agencyUsers;


        public AgencyUsersActivatePlugin(ILogger<AgencyUsersActivatePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(AgencyUsersActivatePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => packet.Cmd.Dto.Ids.Contains(m.Id)).FlexNoTracking().ToListAsync();

            _agencyUsers = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => packet.Cmd.Dto.Ids.Contains(m.Id))
                                .IncludeAgencyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.AgencyUsersActivate(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug($"{typeof(AgencyUser).Name} with {packet.Cmd.Dto.Ids.ToString()} updated into Database");

                    await GenerateAndSendAuditEventAsync(packet);

                    EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    _logger.LogWarning($"No records updated for {typeof(AgencyUser).Name} with {packet.Cmd.Dto.Ids.ToString()}");
                }
            }
            else
            {
                _logger.LogWarning($"{typeof(AgencyUser).Name} with {packet.Cmd.Dto.Ids.ToString()} not found in Database");
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(AgencyUsersActivatePostBusDataPacket packet)
        {
            string jsonPatch;
            foreach (var obj in _model)
            {
                jsonPatch = _diffGenerator.GenerateDiff(_agencyUsers.Where(w => w.Id == obj.Id).FirstOrDefault(), obj);
                _auditData = new AuditEventData(
                    EntityId: obj?.Id,
                    EntityType: AuditedEntityTypeEnum.Agent.Value,
                    Operation: AuditOperationEnum.Edit.Value,
                    JsonPatch: jsonPatch,
                    InitiatorId: _flexAppContext?.UserId,
                    TenantId: _flexAppContext?.TenantId,
                    ClientIP: _flexAppContext?.ClientIP
                );

                EventCondition = CONDITION_ONAUDITREQUEST;
                await this.Fire(EventCondition, packet.FlexServiceBusContext);
            }
        }
    }
}