using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersActivatePlugin : FlexiPluginBase, IFlexiPlugin<CompanyUsersActivatePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19940dc6c473c6dc52aed2c62ec41e";
        public override string FriendlyName { get; set; } = "CompanyUsersActivatePlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<CompanyUsersActivatePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<CompanyUser>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<CompanyUser>? _companyUsers;

        public CompanyUsersActivatePlugin(ILogger<CompanyUsersActivatePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(CompanyUsersActivatePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.Ids.Contains(m.Id)).FlexNoTracking().ToListAsync();

            _companyUsers = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.Ids.Contains(m.Id))
                                .IncludeCompanyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.CompanyUsersActivate(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug($"{typeof(CompanyUser).Name} with {packet.Cmd.Dto.Ids.ToString()} updated into Database");

                    await GenerateAndSendAuditEventAsync(packet);

                    EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    _logger.LogWarning($"No records updated for {typeof(CompanyUser).Name} with {packet.Cmd.Dto.Ids.ToString()}");
                }
            }
            else
            {
                _logger.LogWarning($"{typeof(CompanyUser).Name} with {packet.Cmd.Dto.Ids.ToString()} not found in Database");
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(CompanyUsersActivatePostBusDataPacket packet)
        {
            string jsonPatch;
            foreach (var obj in _model)
            {
                jsonPatch = _diffGenerator.GenerateDiff(_companyUsers.Where(w => w.Id == obj.Id).FirstOrDefault(), obj);
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