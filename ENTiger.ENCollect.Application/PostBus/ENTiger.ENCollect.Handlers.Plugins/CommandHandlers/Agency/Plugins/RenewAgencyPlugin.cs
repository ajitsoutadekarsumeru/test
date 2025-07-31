using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class RenewAgencyPlugin : FlexiPluginBase, IFlexiPlugin<RenewAgencyPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a131f20912cddde24a558004e90dca1";
        public override string FriendlyName { get; set; } = "RenewAgencyPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<RenewAgencyPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<Agency>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<Agency>? _agency;

        public RenewAgencyPlugin(ILogger<RenewAgencyPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(RenewAgencyPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            string loggedInUserId = _flexAppContext.UserId;

            _model = await _repoFactory.GetRepo().FindAll<Agency>().Where(m => (packet.Cmd.Dto.agencyIds.Contains(m.Id))).ToListAsync();

            _agency = await _repoFactory.GetRepo().FindAll<Agency>().Where(m => packet.Cmd.Dto.agencyIds.Contains(m.Id))
                                .IncludeAgencyWorkflow().FlexNoTracking().ToListAsync();

            IList<AgencyWorkflowState> workFlowStates = await _repoFactory.GetRepo().FindAllObjectWithState<AgencyWorkflowState>().Where(a => packet.Cmd.Dto.agencyIds.Contains(a.TFlexId)).ToListAsync();
            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    var state = workFlowStates.Where(i => i.TFlexId == obj.Id
                                           && (i.GetType() == typeof(AgencyPendingApproval) || i.GetType() == typeof(AgencyPendingApprovalWithDeferrals)))
                                           .OrderByDescending(x => x.StateChangedDate).FirstOrDefault();

                    if (state != null && state.GetType() == typeof(AgencyPendingApprovalWithDeferrals))
                    {
                        obj.AgencyWorkflowState = state;// _flexHost.GetFlexStateInstance<AgencyPendingApprovalWithDeferrals>();//.SetStateChangedBy(loggedInUserId).SetTFlexId(this.Id);
                    }
                    else
                    {
                        obj.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencyPendingApproval>();//.SetStateChangedBy(loggedInUserId).SetTFlexId(this.Id);
                    }

                    obj.RenewAgency(_flexAppContext?.UserId, obj.AgencyWorkflowState);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Agency).Name, packet.Cmd.Dto.agencyIds.ToString());

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Agency).Name, packet.Cmd.Dto.agencyIds.ToString());
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Agency).Name, packet.Cmd.Dto.agencyIds.ToString());
            }
        }

        private async Task GenerateAndSendAuditEventAsync(RenewAgencyPostBusDataPacket packet)
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