using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class MakeDormantCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<MakeDormantCompanyUserPostBusDataPacket>
    {
        public override string Id { get; set; } = "3d99fd8dd56b4601bcbe94cf94f4b624";
        public override string FriendlyName { get; set; } = "MakeDormantCompanyUserPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<MakeDormantCompanyUserPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly SystemUserSettings _systemUserSettings;

        protected List<CompanyUser> _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<CompanyUser> _CompanyUsers;

        public MakeDormantCompanyUserPlugin(ILogger<MakeDormantCompanyUserPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator, IOptions<SystemUserSettings> systemUserSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
            _systemUserSettings = systemUserSettings.Value;
        }
        public virtual async Task Execute(MakeDormantCompanyUserPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.companyUserIds.Contains(m.Id)).ToListAsync();

            _CompanyUsers = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.companyUserIds.Contains(m.Id))
                       .IncludeCompanyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.MakeDormantCompanyUser(_flexAppContext?.UserId);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());
                }

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(MakeDormantCompanyUserPostBusDataPacket packet)
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
                                InitiatorId: _systemUserSettings.SystemUserId,
                                TenantId: _flexAppContext?.TenantId,
                                ClientIP: _flexAppContext?.ClientIP
                            );

                EventCondition = CONDITION_ONAUDITREQUEST;
                await this.Fire(EventCondition, packet.FlexServiceBusContext);
            }
        }
    }
}