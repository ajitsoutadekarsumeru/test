using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class DeactivateCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<DeactivateCompanyUserPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12da592b88dc9eff3953a250303aed";
        public override string FriendlyName { get; set; } = "DeactivateCompanyUserPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<DeactivateCompanyUserPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<CompanyUser> _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<CompanyUser> _CompanyUsers;

        public DeactivateCompanyUserPlugin(ILogger<DeactivateCompanyUserPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(DeactivateCompanyUserPostBusDataPacket packet)
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
                    obj.DeactivateCompanyUser(_flexAppContext?.UserId, packet.Cmd.Dto.description);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                    await UpdateUserAttendanceAsLogoutAsync(obj.Id);
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
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.companyUserIds.ToString());
            }

            await Task.CompletedTask;
        }

        private async Task GenerateAndSendAuditEventAsync(DeactivateCompanyUserPostBusDataPacket packet)
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
        /// <summary>
        /// Updates the latest user attendance log by marking the session as logged out.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose attendance needs to be updated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method fetches the most recent <see cref="UserAttendanceLog"/> entry for the specified user,
        /// updates it to reflect a logout action by setting <c>IsSessionValid</c> to false, 
        /// and records logout time and coordinates. If no attendance record is found, it logs a warning.
        /// </remarks>
        public async Task UpdateUserAttendanceAsLogoutAsync(string userId)
        {
            var repo = _repoFactory.GetRepo();

            // Fetch latest attendance log entry for the user
            var log = await repo.FindAll<UserAttendanceLog>()
                                .ByUserAttendanceLogUserId(userId)
                                .OrderByDescending(t => t.CreatedDate)
                                .FirstOrDefaultAsync();

            if (log == null)
            {
                _logger.LogWarning("No attendance log found for user {UserId}", userId);
                return;
            }

            // Update attendance log as logout
            log.IsSessionValid = false;
            log.LogOutTime = DateTime.Now;

            // Reuse login coordinates if logout location is not separately tracked
            log.LogOutLatitude ??= log.LogInLatitude;
            log.LogOutLongitude ??= log.LogInLongitude;

            log.SetLastModifiedBy(userId);
            log.SetLastModifiedDate(DateTimeOffset.Now);
            log.SetModified();

            repo.InsertOrUpdate(log);
            int result = await repo.SaveAsync();

            _logger.LogInformation("User logout attendance updated for {UserId}, rows affected: {Result}", userId, result);
        }
    }
}