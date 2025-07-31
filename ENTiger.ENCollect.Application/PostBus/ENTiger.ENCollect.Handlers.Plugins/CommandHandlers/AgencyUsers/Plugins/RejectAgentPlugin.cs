using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class RejectAgentPlugin : FlexiPluginBase, IFlexiPlugin<RejectAgentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12e79e98f756158e58dc6eab6e2dee";
        public override string FriendlyName { get; set; } = "RejectAgentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<RejectAgentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<AgencyUser>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<AgencyUser>? _agencyUsers;

        public RejectAgentPlugin(ILogger<RejectAgentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(RejectAgentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => packet.Cmd.Dto.AgentIds.Contains(m.Id)).ToListAsync();

            _agencyUsers = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => packet.Cmd.Dto.AgentIds.Contains(m.Id))
                            .IncludeAgencyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.RejectAgent(_flexAppContext?.UserId, packet.Cmd.Dto.Description);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(AgencyUser).Name, packet.Cmd.Dto.AgentIds?.ToString());

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(AgencyUser).Name, packet.Cmd.Dto.AgentIds?.ToString());
                }

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(AgencyUser).Name, packet.Cmd.Dto.AgentIds?.ToString());
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(RejectAgentPostBusDataPacket packet)
        {
            string jsonPatch;
            foreach (var obj in _model)
            {
                jsonPatch = _diffGenerator.GenerateDiff(_agencyUsers?.Where(w => w.Id == obj.Id).FirstOrDefault(), obj);
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