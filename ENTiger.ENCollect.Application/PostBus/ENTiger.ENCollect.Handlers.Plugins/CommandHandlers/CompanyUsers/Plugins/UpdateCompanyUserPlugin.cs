using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using Newtonsoft.Json;
using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class UpdateCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCompanyUserPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12da52c61ce62f10d22af33ef9c13e";
        public override string FriendlyName { get; set; } = "UpdateCompanyUserPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateCompanyUserPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CompanyUser? _model;
        protected CompanyUser? user;
        protected FlexAppContextBridge? _flexAppContext;
        private string authUrl = string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly UserFieldSettings _userFieldSettings;
        private readonly IApiHelper _apiHelper;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected CompanyUser? CompanyUser;
        public UpdateCompanyUserPlugin(ILogger<UpdateCompanyUserPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
                , IOptions<AuthSettings> authSettings, IOptions<UserFieldSettings> userFieldSettings, IApiHelper apiHelper, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _userFieldSettings = userFieldSettings.Value;
            _apiHelper = apiHelper;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(UpdateCompanyUserPostBusDataPacket packet)
        {
            _logger.LogInformation("UpdateCompanyUserPlugin : Start");
            authUrl = _authSettings.AuthUrl;
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            string tenantId = _flexAppContext.TenantId;
            string partyId = _flexAppContext.UserId;

            _model = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => m.Id == packet.Cmd.Dto.Id)
                        .IncludeUserProductScope().IncludeUserGeoScope().IncludeUserBucketScope()
                        .IncludeLanguagesStaff().IncludeCompanyUserWFState()
                        .IncludeDesignation().IncludeUserPerformanceBand().IncludeUserPersona()
                        .CompanyUserIncludePlaceOfWork().IncludeAgencyWallet()
                        .FlexNoTracking().FirstOrDefaultAsync();

            user = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => m.Id == packet.Cmd.Dto.Id)
                        .IncludeUserProductScope().IncludeUserGeoScope().IncludeUserBucketScope()
                        .IncludeLanguagesStaff().IncludeCompanyUserWFState()
                        .IncludeDesignation().IncludeUserPerformanceBand().IncludeUserPersona()
                        .CompanyUserIncludePlaceOfWork().IncludeAgencyWallet()
                        .FlexNoTracking().FirstOrDefaultAsync();

            CompanyUser = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => m.Id == packet.Cmd.Dto.Id)
                        .IncludeUserProductScope().IncludeUserGeoScope().IncludeUserBucketScope()
                        .IncludeLanguagesStaff().IncludeCompanyUserWFState()
                        .IncludeDesignation().IncludeUserPerformanceBand().IncludeUserPersona()
                        .CompanyUserIncludePlaceOfWork().IncludeAgencyWallet()
                        .FlexNoTracking().FirstOrDefaultAsync();

            if (_model != null)
            {
                _logger.LogInformation("UpdateCompanyUserPlugin : UserId - " + _model.UserId);
                string customId = _model.UserId;
                //var user = _model;
                string existingEmail = user.PrimaryEMail;
                _model.UpdateCompanyUser(packet.Cmd, user);

                _model.UserId = customId;
                _logger.LogInformation("UpdateCompanyUserPlugin : UserId - " + _model.UserId);
                List<Accountability> accountabilities = new List<Accountability>();
                accountabilities = await GetAccountabilities(packet.Cmd.Dto, _model.Id, partyId);
                if (!_userFieldSettings.EnableUpdateEmail)
                {
                    _model.PrimaryEMail = user.PrimaryEMail;
                    _model.DomainId = user.DomainId;
                }
                if (!_userFieldSettings.EnableUpdateMobileNo)
                {
                    _model.PrimaryMobileNumber = user.PrimaryMobileNumber;
                }
                _repoFactory.GetRepo().InsertOrUpdate(_model);
                foreach (Accountability accountability in accountabilities)
                {
                    _repoFactory.GetRepo().InsertOrUpdate(accountability);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CompanyUser).Name, _model.Id);
                    if (_userFieldSettings.EnableUpdateEmail && _userFieldSettings.EnableUpdateMobileNo)
                    {
                        await UpdateAuthEmail(existingEmail, _model.PrimaryEMail, tenantId);
                    }

                    await GenerateAndSendAuditEventAsync(packet);

                    await UpdateUserAttendanceAsLogoutAsync(_model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CompanyUser).Name, _model.Id);
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CompanyUser).Name, packet.Cmd.Dto.Id);
            }
            _logger.LogInformation("UpdateCompanyUserPlugin : End | EventCondition - " + EventCondition);
        }

        private async Task GenerateAndSendAuditEventAsync(UpdateCompanyUserPostBusDataPacket packet)
        {
            string jsonPatch = _diffGenerator.GenerateDiff(CompanyUser, _model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
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

        private async Task<List<Accountability>> GetAccountabilities(UpdateCompanyUserDto model, string userId, string partyId)
        {
            _logger.LogInformation("UpdateCompanyUserPlugin : GetAccountabilities - Start");
            string accountabilityTypeId = string.Empty;
            List<string> accountabilityTypeList = new List<string>();
            List<Accountability> accountabilities = new List<Accountability>();
            if (model.Roles != null && model.Roles.Count > 0)
            {
                _logger.LogInformation("UpdateCompanyUserPlugin : Roles Count - " + model.Roles.Count);
                foreach (var role in model.Roles)
                {
                    _logger.LogInformation("UpdateCompanyUserPlugin : DepartmentId - " + role.DepartmentId + " | DesignationId - " + role.DesignationId);
                    var departmentTypeID = await _repoFactory.GetRepo().FindAll<Department>().Where(d => d.Id == role.DepartmentId).Select(a => a.DepartmentTypeId).FirstOrDefaultAsync();

                    var designationtypeid = await _repoFactory.GetRepo().FindAll<Designation>().Where(x => x.Id == role.DesignationId).Select(a => a.DesignationTypeId).FirstOrDefaultAsync();

                    accountabilityTypeId = "BankTo" + departmentTypeID + designationtypeid;
                    _logger.LogInformation("UpdateCompanyUserPlugin : AccountabilityTypeId - " + accountabilityTypeId);
                    if (!accountabilityTypeList.Contains(accountabilityTypeId))
                    {
                        accountabilityTypeList.Add(accountabilityTypeId);
                        Accountability accountability = new Accountability();
                        accountability.CommisionerId = model.BaseBranchId;
                        accountability.ResponsibleId = model.Id;
                        accountability.AccountabilityTypeId = accountabilityTypeId;
                        accountability.SetAddedOrModified();
                        accountability.SetCreatedBy(partyId);
                        accountability.SetLastModifiedBy(partyId);
                        accountabilities.Add(accountability);
                    }
                }
                List<Accountability> lstExistingAccountabilities = new List<Accountability>();
                lstExistingAccountabilities = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.ResponsibleId == _model.Id).FlexNoTracking().ToListAsync();
                lstExistingAccountabilities?.ForEach(a => a.SetDeleted());
                accountabilities.AddRange(lstExistingAccountabilities);
            }
            return accountabilities;
        }

        private async Task UpdateAuthEmail(string existingEmail, string newEmail, string tenantId)
        {
            if (existingEmail != newEmail)
            {
                _logger.LogWarning("UpdateCompanyUserPlugin : ExistingEmail - " + existingEmail + " != " + "NewEmail - " + newEmail);
                string authapiUrl;
                authapiUrl = authUrl;

                _logger.LogWarning("UpdateCompanyUserPlugin : authapiUrl - " + authapiUrl);

                var data = new
                {
                    email = tenantId + "_" + existingEmail,
                    newemail = tenantId + "_" + newEmail,
                    password = _authSettings.Password,
                    confirmPassword = _authSettings.ConfirmPassword,
                    firstName = "test",
                    middleName = "",
                    lastName = ""
                };

                string jsonPayload = JsonConvert.SerializeObject(data);
                var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
                string authUserId = await checkExistName.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(authUserId))
                {
                    _logger.LogWarning("UpdateCompanyUserPlugin : Auth User does not exist");
                }
                else
                {
                    var checkUpdateEmail = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/UpdateEmail", HttpMethod.Post);
                    string UpdatedEmail = await checkUpdateEmail.Content.ReadAsStringAsync();
                    _logger.LogInformation("UpdateCompanyUserPlugin : Auth User updated sucessfully to " + UpdatedEmail);
                }
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