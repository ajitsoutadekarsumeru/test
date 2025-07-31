using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountScopeConfigurationPlugin : FlexiPluginBase, IFlexiPlugin<UpdateAccountScopeConfigurationPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a185d0721f74f1b25c249af8632fc3d";
        public override string FriendlyName { get; set; } = "UpdateRoleBasedSearchPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<UpdateAccountScopeConfigurationPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<AccountScopeConfiguration>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        List<AccountScopeConfiguration>? roleAccountScopes;

        public UpdateAccountScopeConfigurationPlugin(ILogger<UpdateAccountScopeConfigurationPlugin> logger,
            IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(UpdateAccountScopeConfigurationPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var listofIds = packet.Cmd.Dto.SearchScopes.Select(a => a.Id).ToList();
            roleAccountScopes = await _repoFactory.GetRepo().FindAll<AccountScopeConfiguration>().Where(m => listofIds.Contains(m.Id)).FlexNoTracking().ToListAsync();

            await FetchRoleScopesAsync(packet);

            if (_model != null)
            {
                UpdateRoleScopes(packet);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} updated into Database: ", typeof(AccountScopeConfiguration).Name);

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} ", typeof(AccountScopeConfiguration).Name);
                }
            }
            else
            {
                _logger.LogWarning("{Entity} not found in Database: ", typeof(AccountScopeConfiguration).Name);
            }
        }

        private async Task GenerateAndSendAuditEventAsync(UpdateAccountScopeConfigurationPostBusDataPacket packet)
        {
            foreach (AccountScopeConfiguration newScope in _model)
            {
                AccountScopeConfiguration oldScope = roleAccountScopes?.Where(w => w.Id == newScope.Id).FirstOrDefault();
                string jsonPatch = _diffGenerator.GenerateDiff(oldScope, newScope);
                _auditData = new AuditEventData(
                    EntityId: newScope?.Id,
                    EntityType: AuditedEntityTypeEnum.AccountSearchScope.Value,
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

        private async Task FetchRoleScopesAsync(UpdateAccountScopeConfigurationPostBusDataPacket packet)
        {
            var listofIds = packet.Cmd.Dto.SearchScopes.Select(a => a.Id).ToList();
            _model = await _repoFactory.GetRepo().FindAll<AccountScopeConfiguration>()
                .Where(m => listofIds.Contains(m.Id)).ToListAsync();
        }

        private void UpdateRoleScopes(UpdateAccountScopeConfigurationPostBusDataPacket packet)
        {
            foreach (var obj in _model)
            {
                //Update the scopes
                var dto = packet.Cmd.Dto.SearchScopes.Where(a => a.Id == obj.Id).FirstOrDefault();
                dto.SetAppContext(_flexAppContext);
                
                obj.Update(dto);
                _repoFactory.GetRepo().InsertOrUpdate(obj);
            }
        }
    }
}