using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class UpdateAgentPlugin : FlexiPluginBase, IFlexiPlugin<UpdateAgentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12e7a17c0cd1b21349f816eb7480ca";
        public override string FriendlyName { get; set; } = "UpdateAgentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateAgentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected AgencyUser? _model;
        protected AgencyUser? user;
        protected FlexAppContextBridge? _flexAppContext;
        private string authUrl = string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly UserFieldSettings _userFieldSettings;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected readonly IApiHelper _apiHelper;
        protected AgencyUser? agencyUser;

        public UpdateAgentPlugin(ILogger<UpdateAgentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings
                , IOptions<UserFieldSettings> userFieldSettings, IDiffGenerator diffGenerator, IApiHelper apiHelper)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _userFieldSettings = userFieldSettings.Value;
            _diffGenerator = diffGenerator;
            _apiHelper = apiHelper;
        }

        public virtual async Task Execute(UpdateAgentPostBusDataPacket packet)
        {
            _logger.LogInformation("UpdateAgentPlugin : Start");
            authUrl = _authSettings.AuthUrl;
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);

            string tenantId = _flexAppContext.TenantId;
            string partyId = _flexAppContext.UserId;

            _model = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => m.Id == packet.Cmd.Dto.Id)
                        .IncludeAgencyUserWorkflow()
                        .IncludeUserProductScope().IncludeUserGeoScope().IncludeUserBucketScope()
                        .IncludeAgencyUserPlaceOfWork().IncludeAgencyUserDesignation()
                        .IncludeAgencyUserAddress().IncludeAgencyWallet().IncludeAgencyUserDesignationName()
                        .FlexNoTracking().FirstOrDefaultAsync();

            user = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => m.Id == packet.Cmd.Dto.Id)
                        .IncludeAgencyUserWorkflow()
                        .IncludeUserProductScope().IncludeUserGeoScope().IncludeUserBucketScope()
                        .IncludeAgencyUserPlaceOfWork().IncludeAgencyUserDesignation()
                        .IncludeAgencyUserAddress().IncludeAgencyWallet().IncludeAgencyUserDesignationName()
                        .FlexNoTracking().FirstOrDefaultAsync();

            agencyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(m => m.Id == packet.Cmd.Dto.Id)
                                .IncludeAgencyUserWorkflow()
                                .IncludeUserProductScope().IncludeUserGeoScope().IncludeUserBucketScope()
                                .IncludeAgencyUserPlaceOfWork().IncludeAgencyUserDesignation()
                                .IncludeAgencyUserAddress().IncludeAgencyWallet().IncludeAgencyUserDesignationName()
                                .FlexNoTracking().FirstOrDefaultAsync();

            if (_model != null && user != null)
            {
                _logger.LogInformation("From DB PlaceOfWork");
                string existingEmail = user.PrimaryEMail;

                _model.UpdateAgent(packet.Cmd);
                if (packet.Cmd.Dto.isSaveAsDraft)
                {
                    _model.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserSavedAsDraft>().SetTFlexId(this.Id).SetStateChangedBy(partyId ?? "");
                }
                else
                {
                    _model.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserPendingApproval>().SetTFlexId(this.Id).SetStateChangedBy(partyId ?? "");
                }

                List<AgencyUserDesignation> designations = new List<AgencyUserDesignation>();
                List<UserProductScope> productScopes = new List<UserProductScope>();
                List<UserGeoScope> geoScopes = new List<UserGeoScope>();
                List<UserBucketScope> bucketScopes = new List<UserBucketScope>();
                List<AgencyUserPlaceOfWork> placelist = new List<AgencyUserPlaceOfWork>();

                if (user.Designation != null)
                {
                    foreach (var designation in user.Designation)
                    {
                        var obj = _model?.Designation?.Where(c => c.Id == designation.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            designation.DepartmentId = obj.DepartmentId;
                            designation.DesignationId = obj.DesignationId;
                            designation.IsPrimaryDesignation = obj.IsPrimaryDesignation;
                            designation.SetLastModifiedBy(partyId);
                            designation.SetLastModifiedDate(DateTimeOffset.Now);
                        }
                        else
                        {
                            designation.SetAsDeleted(true);
                        }
                        designation.SetModified();
                        designations.Add(designation);
                    }
                }

                if (user.ProductScopes != null)
                {
                    foreach (var scope in user.ProductScopes)
                    {
                        var obj = _model?.ProductScopes?.Where(c => c.Id == scope.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            scope.ProductScopeId = obj.ProductScopeId;
                            scope.SetLastModifiedBy(partyId);
                            scope.SetLastModifiedDate(DateTimeOffset.Now);
                        }
                        else
                        {
                            scope.SetAsDeleted(true);
                        }
                        scope.SetModified();
                        productScopes.Add(scope);
                    }
                }
                if (user.GeoScopes != null)
                {
                    foreach (var scope in user.GeoScopes)
                    {
                        var obj = _model?.GeoScopes?.Where(c => c.Id == scope.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            scope.GeoScopeId = obj.GeoScopeId;
                            scope.SetLastModifiedBy(partyId);
                            scope.SetLastModifiedDate(DateTimeOffset.Now);
                        }
                        else
                        {
                            scope.SetAsDeleted(true);
                        }
                        scope.SetModified();
                        geoScopes.Add(scope);
                    }
                }
                if (user.BucketScopes != null)
                {
                    foreach (var scope in user.BucketScopes)
                    {
                        var obj = _model?.BucketScopes?.Where(c => c.Id == scope.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            scope.BucketScopeId = obj.BucketScopeId;
                            scope.SetLastModifiedBy(partyId);
                            scope.SetLastModifiedDate(DateTimeOffset.Now);
                        }
                        else
                        {
                            scope.SetAsDeleted(true);
                        }
                        scope.SetModified();
                        bucketScopes.Add(scope);
                    }
                }

                if (user.PlaceOfWork != null)
                {
                    _logger.LogInformation("user.PlaceOfWork : Not Null");
                    foreach (var place in user.PlaceOfWork)
                    {
                        var obj = _model?.PlaceOfWork?.Where(c => c.Id == place.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            place.PIN = obj.PIN;
                        }
                        else
                        {
                            place.SetAsDeleted(true);
                        }
                        place.SetModified();
                        place.SetLastModifiedBy(partyId);
                        place.SetLastModifiedDate(DateTimeOffset.Now);
                        _logger.LogInformation("PlaceOfWork : Added into list");
                        placelist.Add(place);
                    }
                }

                foreach (var obj in _model.Designation.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    obj.SetCreatedBy(partyId);
                    obj.SetLastModifiedBy(partyId);
                    obj.SetAddedOrModified();
                    designations.Add(obj);
                }
                foreach (var obj in _model.ProductScopes.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    obj.SetCreatedBy(partyId);
                    obj.SetLastModifiedBy(partyId);
                    obj.SetAddedOrModified();
                    productScopes.Add(obj);
                }
                foreach (var obj in _model.GeoScopes.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    obj.SetCreatedBy(partyId);
                    obj.SetLastModifiedBy(partyId);
                    obj.SetAddedOrModified();
                    geoScopes.Add(obj);
                }
                foreach (var obj in _model.BucketScopes.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    obj.SetCreatedBy(partyId);
                    obj.SetLastModifiedBy(partyId);
                    obj.SetAddedOrModified();
                    bucketScopes.Add(obj);
                }
                foreach (var obj in _model.PlaceOfWork.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    obj.SetCreatedBy(partyId);
                    obj.SetLastModifiedBy(partyId);
                    obj.SetAddedOrModified();
                    placelist.Add(obj);
                }

                _model.Designation = designations;
                _model.ProductScopes = productScopes;
                _model.GeoScopes = geoScopes;
                _model.BucketScopes = bucketScopes;
                _model.PlaceOfWork = placelist;

                _model.UserId = user.UserId;
                _model.SetCustomId(user.CustomId);
                _model.AgencyId = packet.Cmd.Dto.AgencyId;
                _model.SetCreatedBy(user.CreatedBy);
                _model.SetCreatedDate(user.CreatedDate);
                _model.SetLastModifiedDate(DateTimeOffset.Now);
                _model.SetLastModifiedBy(partyId);
                _model.CreditAccountDetails.SetAddedOrModified();
                _model.CreditAccountDetails.SetLastModifiedBy(partyId);
                _model.Address.SetAddedOrModified();
                _model.Address.SetLastModifiedBy(partyId);

                _model.UserLoad = packet.Cmd.Dto.load;
                _model.Experience = packet.Cmd.Dto.Experience;

                _model.AgencyUserIdentifications = new List<AgencyUserIdentification>();
                _model.AgencyUserIdentifications = await EditProfileIdentificationAsync(packet.Cmd.Dto, tenantId);

                List<Accountability> accountabilities = new List<Accountability>();
                accountabilities = await GetAccountabilitiesAsync(packet.Cmd.Dto, partyId);

                _repoFactory.GetRepo().InsertOrUpdate(_model);
                foreach (Accountability accountability in accountabilities)
                {
                    _repoFactory.GetRepo().InsertOrUpdate(accountability);
                }
                if (!_userFieldSettings.EnableUpdateEmail)
                {
                    _model.PrimaryEMail = user.PrimaryEMail;
                }
                if (!_userFieldSettings.EnableUpdateMobileNo)
                {
                    _model.PrimaryMobileNumber = user.PrimaryMobileNumber;
                }
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(AgencyUser).Name, _model.Id);

                    if (_userFieldSettings.EnableUpdateEmail && _userFieldSettings.EnableUpdateMobileNo)
                    {
                        await UpdateAuthEmail(existingEmail, _model.PrimaryEMail, tenantId);
                    }

                    await GenerateAndSendAuditEventAsync(packet);

                    await UpdateUserAttendanceAsLogoutAsync(_model.Id);

                    EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(AgencyUser).Name, _model.Id);
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(AgencyUser).Name, packet.Cmd.Dto.Id);
            }
            _logger.LogInformation("UpdateAgentPlugin : End | EventCondition - " + EventCondition);
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(UpdateAgentPostBusDataPacket packet)
        {
            string jsonPatch = _diffGenerator.GenerateDiff(agencyUser, _model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
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

        private async Task<List<AgencyUserIdentification>> EditProfileIdentificationAsync(UpdateAgentDto model, string tenantId)
        {
            _logger.LogInformation("UpdateAgentPlugin : EditProfileIdentification - Start");
            List<AgencyUserIdentification> tflexidentification = new List<AgencyUserIdentification>();

            if (model.profileIdentification != null)
            {
                foreach (var apimodel in model.profileIdentification)
                {
                    AgencyUserIdentification Identification = new AgencyUserIdentification();
                    Identification.SetId(apimodel.Id);
                    Identification.DeferredTillDate = apimodel.DeferredTillDate;
                    Identification.IsDeferred = apimodel.IsDeferred;
                    Identification.IsWavedOff = apimodel.IsWavedOff;
                    Identification.TFlexIdentificationDocTypeId = apimodel.IdentificationDocTypeId;
                    Identification.TFlexIdentificationTypeId = apimodel.IdentificationTypeId;
                    Identification.SetAddedOrModified();

                    AgencyUserIdentificationDoc docs = await _repoFactory.GetRepo().FindAll<AgencyUserIdentificationDoc>().Where(x => x.Id == apimodel.IdentificationDocId).FirstOrDefaultAsync();
                    if (docs != null)
                    {
                        Identification.TFlexIdentificationDocs = new List<AgencyUserIdentificationDoc>();
                        docs.TFlexIdentificationId = Identification.Id;
                        docs.SetAddedOrModified();
                        Identification.TFlexIdentificationDocs.Add(docs);
                    }
                    tflexidentification.Add(Identification);
                }
            }
            _logger.LogInformation("UpdateAgentPlugin : EditProfileIdentification - End");
            return tflexidentification;
        }

        private async Task<List<Accountability>> GetAccountabilitiesAsync(UpdateAgentDto model, string partyId)
        {
            _logger.LogInformation("UpdateAgentPlugin : GetAccountabilities - Start");
            string accountabilityTypeId = string.Empty;
            List<string> accountabilityTypeList = new List<string>();
            List<Accountability> accountabilities = new List<Accountability>();
            if (model.Roles != null && model.Roles.Count() > 0)
            {
                _logger.LogInformation("UpdateAgentPlugin : Roles Count - " + model.Roles.Count);
                foreach (var role in model.Roles)
                {
                    _logger.LogInformation("UpdateAgentPlugin : DepartmentId - " + role.DepartmentId + " | DesignationId - " + role.DesignationId);
                    var departmentTypeID = await _repoFactory.GetRepo().FindAll<Department>().Where(d => d.Id == role.DepartmentId).Select(a => a.DepartmentTypeId).FirstOrDefaultAsync();

                    var designationtypeid = await _repoFactory.GetRepo().FindAll<Designation>().Where(x => x.Id == role.DesignationId).Select(a => a.DesignationTypeId).FirstOrDefaultAsync();

                    accountabilityTypeId = "AgencyTo" + departmentTypeID + designationtypeid;
                    _logger.LogInformation("UpdateAgentPlugin : AccountabilityTypeId - " + accountabilityTypeId);
                    if (!accountabilityTypeList.Contains(accountabilityTypeId))
                    {
                        accountabilityTypeList.Add(accountabilityTypeId);
                        Accountability accountability = new Accountability();
                        accountability.CommisionerId = model.AgencyId;
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
                foreach (var accountability in lstExistingAccountabilities)
                {
                    accountability.SetDeleted();
                }
                accountabilities.AddRange(lstExistingAccountabilities);
            }
            _logger.LogInformation("UpdateAgentPlugin : GetAccountabilities - End");
            return accountabilities;
        }

        private async Task UpdateAuthEmail(string existingEmail, string newEmail, string tenantId)
        {
            if (existingEmail != newEmail)
            {
                _logger.LogWarning("UpdateAgentPlugin : ExistingEmail - " + existingEmail + " != " + "NewEmail - " + newEmail);
                string authapiUrl;
                authapiUrl = authUrl;// _repoFactory.GetRepo().FindAll<TenantConfiguration>().Where(x => x.Id == _tenantId).FirstOrDefault().AuthUrl;

                _logger.LogWarning("UpdateAgentPlugin : authapiUrl - " + authapiUrl);

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
                    _logger.LogWarning("UpdateAgentPlugin : Auth User does not exist");
                }
                else
                {
                    var checkUpdateEmail = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/UpdateEmail", HttpMethod.Post);
                    string UpdatedEmail = await checkUpdateEmail.Content.ReadAsStringAsync();
                    _logger.LogInformation("UpdateAgentPlugin : Auth User updated sucessfully to " + UpdatedEmail);
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