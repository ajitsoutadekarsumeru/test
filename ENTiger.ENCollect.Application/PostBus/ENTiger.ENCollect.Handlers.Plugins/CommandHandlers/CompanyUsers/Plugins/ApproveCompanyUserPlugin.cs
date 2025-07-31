using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ApproveCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<ApproveCompanyUserPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12da55c8cad5dc734a20f14b730c17";
        public override string FriendlyName { get; set; } = "ApproveCompanyUserPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ApproveCompanyUserPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected List<CompanyUser> _model;
        protected FlexAppContextBridge? _flexAppContext;
        private List<string> Ids;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<CompanyUser> _CompanyUsers;

        public ApproveCompanyUserPlugin(ILogger<ApproveCompanyUserPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(ApproveCompanyUserPostBusDataPacket packet)
        {
            _logger.LogInformation("ApproveCompanyUserPlugin : Start");
            Ids = new List<string>();
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.companyUserIds.Contains(m.Id)).ToListAsync();

            _CompanyUsers = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.companyUserIds.Contains(m.Id))
                       .IncludeCompanyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                Ids = _model.Select(m => m.Id).ToList();
                foreach (var obj in _model)
                {
                    obj.ApproveCompanyUser(_flexAppContext?.UserId, packet.Cmd.Dto.description);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());

                    await GenerateAndSendAuditEventAsync(packet);

                    EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());
            }
            _logger.LogInformation("ApproveCompanyUserPlugin : End | EventCondition - " + EventCondition);
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(ApproveCompanyUserPostBusDataPacket packet)
        {
            string jsonPatch;
            foreach (var obj in _model)
            {
                jsonPatch = _diffGenerator.GenerateDiff(_CompanyUsers.Where(w => w.Id == obj.Id).FirstOrDefault(), obj);
                _auditData = new AuditEventData(
                                EntityId: obj?.Id,
                                EntityType: AuditedEntityTypeEnum.Staff.Value,
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