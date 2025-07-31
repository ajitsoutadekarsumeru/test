using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class EnableCompanyUsersPlugin : FlexiPluginBase, IFlexiPlugin<EnableCompanyUsersPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1a0a28ed7d46fa68b5c9f8cc692de8";
        public override string FriendlyName { get; set; } = "EnableCompanyUsersPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<EnableCompanyUsersPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;

        protected List<CompanyUser> _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<CompanyUser> _CompanyUsers;

        public EnableCompanyUsersPlugin(ILogger<EnableCompanyUsersPlugin> logger, IFlexHost flexHost, RepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(EnableCompanyUsersPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.CompanyUserIds.Contains(m.Id)).ToListAsync();

            _CompanyUsers = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => packet.Cmd.Dto.CompanyUserIds.Contains(m.Id))
                       .IncludeCompanyUserWorkflow().FlexNoTracking().ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.EnableCompanyUsers(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.CompanyUserIds.ToString());
                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CompanyUser).Name, packet.Cmd.Dto.CompanyUserIds.ToString());
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.CompanyUserIds.ToString());
            } 
        }

        private async Task GenerateAndSendAuditEventAsync(EnableCompanyUsersPostBusDataPacket packet)
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