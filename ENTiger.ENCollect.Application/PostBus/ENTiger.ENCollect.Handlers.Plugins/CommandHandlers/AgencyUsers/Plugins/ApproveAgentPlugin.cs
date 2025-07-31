using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ApproveAgentPlugin : FlexiPluginBase, IFlexiPlugin<ApproveAgentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12e79d48e53cab7f72450388a1bdd2";
        public override string FriendlyName { get; set; } = "ApproveAgentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ApproveAgentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<AgencyUser>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<AgencyUser>? _agencyUsers;

        public ApproveAgentPlugin(ILogger<ApproveAgentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(ApproveAgentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => packet.Cmd.Dto.AgentIds.Contains(m.Id)).FlexNoTracking().ToListAsync();

            _agencyUsers = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => packet.Cmd.Dto.AgentIds.Contains(m.Id))
                                .IncludeAgencyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.ApproveAgent(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(AgencyUser).Name, packet.Cmd.Dto.AgentIds.ToString());

                    await GenerateAndSendAuditEventAsync(packet);

                    EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(AgencyUser).Name, packet.Cmd.Dto.AgentIds.ToString());
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(AgencyUser).Name, packet.Cmd.Dto.AgentIds.ToString());
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(ApproveAgentPostBusDataPacket packet)
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