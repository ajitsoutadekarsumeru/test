using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class DeactivateAgencyPlugin : FlexiPluginBase, IFlexiPlugin<DeactivateAgencyPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a131872449a935d911eb6055333ed9f";
        public override string FriendlyName { get; set; } = "DeactivateAgencyPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<DeactivateAgencyPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<Agency>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<Agency>? _agency;

        public DeactivateAgencyPlugin(ILogger<DeactivateAgencyPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(DeactivateAgencyPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<Agency>().Where(m => packet.Cmd.Dto.AgencyIds.Contains(m.Id)).ToListAsync();

            _agency = await _repoFactory.GetRepo().FindAll<Agency>().Where(m => packet.Cmd.Dto.AgencyIds.Contains(m.Id))
                                .IncludeAgencyWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.DeactivateAgency(_flexAppContext?.UserId, packet.Cmd.Dto.DeactivationReason);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Agency).Name, packet.Cmd.Dto.AgencyIds.ToString());

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Agency).Name, packet.Cmd.Dto.AgencyIds.ToString());
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Agency).Name, packet.Cmd.Dto.AgencyIds.ToString());
            }
        }

        private async Task GenerateAndSendAuditEventAsync(DeactivateAgencyPostBusDataPacket packet)
        {
            string jsonPatch;
            foreach (var obj in _model)
            {
                jsonPatch = _diffGenerator.GenerateDiff(_agency.Where(w => w.Id == obj.Id).FirstOrDefault(), obj);
                _auditData = new AuditEventData(
                                EntityId: obj?.Id,
                                EntityType: AuditedEntityTypeEnum.Agency.Value,
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