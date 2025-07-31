using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AckPayInSlipPlugin : FlexiPluginBase, IFlexiPlugin<AckPayInSlipPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138ab4478a7266d9d9b80e5f4826f9";
        public override string FriendlyName { get; set; } = "AckPayInSlipPlugin";

        private readonly ILogger<AckPayInSlipPlugin> _logger;
        private readonly IFlexHost _flexHost;
        private readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected List<string>? payInSlipIds;
        private string EventCondition = "";

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<PayInSlip> payInSlips;
        protected List<PayInSlip> oldPayInSlips;
        protected string payinslipId;

        public AckPayInSlipPlugin(ILogger<AckPayInSlipPlugin> logger,IFlexHost flexHost,IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(AckPayInSlipPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
             payInSlipIds = packet.Cmd.Dto.payInSlipIds?.ToList();

            if (payInSlipIds == null || !payInSlipIds.Any())
            {
                _logger.LogWarning("{Entity} IDs are null or empty. No processing required.", nameof(PayInSlip));
                return;
            }

            payInSlips = await _repoFactory.GetRepo().FindAll<PayInSlip>().ByPaySlipIds(payInSlipIds).ToListAsync();

            oldPayInSlips = await _repoFactory.GetRepo().FindAll<PayInSlip>().ByPaySlipIds(payInSlipIds)
                                    .IncludePayInSlipUserWorkflow().FlexNoTracking().ToListAsync();

            if (!payInSlips.Any())
            {
                _logger.LogWarning("{Entity} with IDs {EntityIds} not found in the database.", nameof(PayInSlip), string.Join(", ", payInSlipIds));
                return;
            }

            foreach (var payInSlip in payInSlips)
            {
                payInSlip.AckPayInSlip(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(payInSlip);
            }

            int records = await _repoFactory.GetRepo().SaveAsync();

            if (records > 0)
            {
                _logger.LogDebug("{Entity} with IDs {EntityIds} successfully updated in the database.", nameof(PayInSlip), string.Join(", ", payInSlipIds));

                await GenerateAndSendAuditEventAsync(packet);

                await GenerateAndSendForProjection(packet);

                EventCondition = CONDITION_ONSUCCESS;

                
            }
            else
            {
                _logger.LogWarning("No records were updated for {Entity} with IDs {EntityIds}.", nameof(PayInSlip), string.Join(", ", payInSlipIds));                
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(AckPayInSlipPostBusDataPacket packet)
        {
            string jsonPatch;
            foreach (var obj in payInSlips)
            {
                jsonPatch = _diffGenerator.GenerateDiff(oldPayInSlips.Where(w => w.Id == obj.Id).FirstOrDefault(), obj);

                _auditData = new AuditEventData(
                                EntityId: obj?.Id,
                                EntityType: AuditedEntityTypeEnum.PayinSlip.Value,
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

        private async Task GenerateAndSendForProjection(AckPayInSlipPostBusDataPacket packet)
        {

            List<string> payInSlipsIds = packet.Cmd.Dto.payInSlipIds.ToList();
            foreach (var payinslipid in payInSlipsIds)
            {
                payinslipId = payinslipid;
                EventCondition = CONDITION_ForProjection;
                await this.Fire(EventCondition, packet.FlexServiceBusContext);
            }
        }
    }
}
