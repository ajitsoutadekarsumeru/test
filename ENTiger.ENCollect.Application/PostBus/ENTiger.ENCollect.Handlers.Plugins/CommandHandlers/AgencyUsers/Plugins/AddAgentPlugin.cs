using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using Newtonsoft.Json;
using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Babel.Licensing;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AddAgentPlugin : FlexiPluginBase, IFlexiPlugin<AddAgentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12d1492413902ce532efc9a05bb630";
        public override string FriendlyName { get; set; } = "AddAgentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddAgentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly ICustomUtility _customUtility;
        protected AgencyUser? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private string authUrl = string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;
        private readonly ILicenseService _licenseService;
        protected AuditEventData _auditData;
        protected decimal _userLimitPercentageUsed;
        private readonly LicenseSettings _licenseSettings;

        public AddAgentPlugin(ILogger<AddAgentPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory, 
            IFlexPrimaryKeyGeneratorBridge pkGenerator, 
            IOptions<AuthSettings> authSettings, 
            ICustomUtility customUtility, IApiHelper apiHelper, ILicenseService licenseService, IOptions<LicenseSettings> loginSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _pkGenerator = pkGenerator;
            _authSettings = authSettings.Value;
            _customUtility = customUtility;
            _apiHelper = apiHelper;
            _licenseService = licenseService;
            _licenseSettings = loginSettings.Value;
        }

        public virtual async Task Execute(AddAgentPostBusDataPacket packet)
        {
            authUrl = _authSettings.AuthUrl;
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<AgencyUser>().AddAgent(packet.Cmd);

            string tenantId = packet.Cmd.Dto.GetAppContext().TenantId;

            string customId = await GetCustomId();
            _model.SetCustomId(customId);
            var userId = await GetUserIdAsync(_model.PrimaryEMail, _model.FirstName, tenantId);
            _model.SetUserId(userId);

            _model.AgencyUserIdentifications = await AddProfileIdentification(packet.Cmd.Dto);

            List<Accountability> accountabilities = new List<Accountability>();
            accountabilities = await GetAccountabilities(_model);

            _logger.LogInformation("AddAgentFFPlugin overwriteAgentId : " + packet.Cmd.Dto.overwriteAgentId);
            if (!string.IsNullOrEmpty(packet.Cmd.Dto.overwriteAgentId))
            {
                string overwriteAgentId = packet.Cmd.Dto.overwriteAgentId;
                _logger.LogInformation("AddAgentFFPlugin start fetch agencyUserExists");
                try
                {
                    AgencyUser? agencyUserExists = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(p => p.Id == overwriteAgentId).FirstOrDefaultAsync();
                    if (agencyUserExists != null)
                    {
                        _logger.LogInformation("AddAgentFFPlugin start update to Exists agencyUser");
                        agencyUserExists.UserId = null;
                        agencyUserExists.SetIsDeleted(true);
                        agencyUserExists.Remarks = "This user overwrite to another agency";
                        _repoFactory.GetRepo().InsertOrUpdate(agencyUserExists);
                        await _repoFactory.GetRepo().SaveAsync();
                        _logger.LogInformation("AddAgentFFPlugin end update to Exists agencyUser");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("AddAgentFFPlugin fetch agencyUserExists error : " + ex);
                }
            }
            _model.TransactionSource = _flexAppContext?.RequestSource;
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            foreach (Accountability accountability in accountabilities)
            {
                _repoFactory.GetRepo().InsertOrUpdate(accountability);
            }
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AgencyUser).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);
                UserTypeEnum userType = Enum.TryParse(_model.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
                _userLimitPercentageUsed = await _licenseService.GetUserTypeLimitPercentageDetailAsync(userType, packet.Cmd.Dto);

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AgencyUser).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(AddAgentPostBusDataPacket packet)
        {
            //for Ignoring reccuring loop use this settings
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string jsonPatch = JsonConvert.SerializeObject(_model, settings);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.Agent.Value,
                Operation: AuditOperationEnum.Add.Value,
                JsonPatch: jsonPatch,
                InitiatorId: _flexAppContext?.UserId,
                TenantId: _flexAppContext?.TenantId,
                ClientIP: _flexAppContext?.ClientIP
            );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<Accountability>> GetAccountabilities(AgencyUser model)
        {
            _logger.LogDebug("AddAgentPlugin : GetAccountabilities - Start");
            List<string> accountabilityTypeList = new List<string>();
            List<Accountability> accountabilities = new List<Accountability>();

            if (model.Designation != null)
            {
                _logger.LogInformation("AddAgentPlugin : Roles Count - " + model.Designation.Count);
                foreach (var role in model.Designation)
                {
                    _logger.LogInformation("AddAgentPlugin : DepartmentId - " + role.DepartmentId + " | DesignationId - " + role.DesignationId);
                    var departmentTypeID = await _repoFactory.GetRepo().FindAll<Department>().Where(x => x.Id == role.DepartmentId).Select(a => a.DepartmentTypeId).FirstOrDefaultAsync();

                    var designationtypeid = await _repoFactory.GetRepo().FindAll<Designation>().Where(x => x.Id == role.DesignationId).Select(a => a.DesignationTypeId).FirstOrDefaultAsync();

                    string accountabilityTypeId = "AgencyTo" + departmentTypeID + designationtypeid;
                    _logger.LogInformation("AddAgentPlugin : AccountabilityTypeId - " + accountabilityTypeId);
                    if (!accountabilityTypeList.Contains(accountabilityTypeId))
                    {
                        accountabilityTypeList.Add(accountabilityTypeId);
                        Accountability accountability = new Accountability();
                        //accountability.SetId(_pkGenerator.GenerateKey());
                        accountability.CommisionerId = model.AgencyId;
                        accountability.ResponsibleId = model.Id;
                        accountability.AccountabilityTypeId = accountabilityTypeId;
                        accountability.SetAddedOrModified();
                        accountability.SetCreatedBy(model.CreatedBy);
                        accountabilities.Add(accountability);
                    }
                }
            }

            _logger.LogDebug("AddAgentPlugin : GetAccountabilities - End");
            return accountabilities;
        }

        private async Task<string> GetCustomId()
        {
            _logger.LogDebug("AddAgentPlugin : GetCustomId - Start");
            string customId = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.AgencyUser.Value);
            _logger.LogDebug("AddAgentPlugin : GetCustomId - End | CustomId - " + customId);
            return customId;
        }

        private async Task<List<AgencyUserIdentification>> AddProfileIdentification(AddAgentDto model)
        {
            _logger.LogDebug("AddAgentPlugin : AddProfileIdentification - Start");
            List<AgencyUserIdentification> tflexidentification = new List<AgencyUserIdentification>();
            foreach (var apimodel in model.profileIdentification)
            {
                AgencyUserIdentification obj = new AgencyUserIdentification();
                obj.DeferredTillDate = apimodel.DeferredTillDate;
                obj.IsDeferred = apimodel.IsDeferred;
                obj.IsWavedOff = apimodel.IsWavedOff;
                obj.TFlexIdentificationDocTypeId = apimodel.IdentificationDocTypeId;
                obj.TFlexIdentificationTypeId = apimodel.IdentificationTypeId;
                obj.SetAddedOrModified();

                AgencyUserIdentificationDoc? docs = await _repoFactory.GetRepo().FindAll<AgencyUserIdentificationDoc>().Where(x => x.Id == apimodel.IdentificationDocId).FirstOrDefaultAsync();
                if (docs != null)
                {
                    obj.TFlexIdentificationDocs = new List<AgencyUserIdentificationDoc>();
                    docs.TFlexIdentificationId = obj.Id;
                    obj.TFlexIdentificationDocs.Add(docs);
                }
                tflexidentification.Add(obj);
            }
            _logger.LogDebug("AddAgentPlugin : AddProfileIdentification - End");
            return tflexidentification;
        }

        private async Task<string> GetUserIdAsync(string email, string name, string tenantId)
        {
            _logger.LogDebug("AddAgentPlugin : GetAuthUserId - Start");
            string userId = "";
            string authapiUrl = authUrl;//_repoFactory.GetRepo().FindAll<TenantConfiguration>().Where(x => x.Id == tenantId).FirstOrDefault().AuthUrl;

            _logger.LogInformation("AddAgentPlugin : GetAuthUserId - TenantId : " + tenantId + " | AuthUrl : " + authapiUrl);

            var data = new
            {
                email = tenantId + "_" + email,
                password = "Abcd@1234",
                confirmPassword = "Abcd@1234",
                firstName = name,
                middleName = "",
                lastName = ""
            };
            string jsonPayload = JsonConvert.SerializeObject(data);
            var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
            string authUserId = await checkExistName.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(authUserId))
            {


                _logger.LogDebug("AddAgentPlugin : GetAuthUserId - Auth User Already Exists");
                string? appId = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(p => p.UserId == authUserId).Select(q => q.Id).FirstOrDefaultAsync();
                if (appId == null)
                {
                    _logger.LogDebug("AddAgentPlugin : GetAuthUserId - Application User Not Found | Delete the Auth User");
                    var dataDelete = new
                    {
                        id = authUserId,
                        email
                    };

                    string deletejsonPayload = JsonConvert.SerializeObject(dataDelete);
                    var deleteExistEmailName = await _apiHelper.SendRequestAsync(deletejsonPayload, authapiUrl + "/api/AccountAPI/DeleteExistEmailName", HttpMethod.Post);
                }
                userId = authUserId;
            }

            var response = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/Register", HttpMethod.Post);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogDebug("AddAgentPlugin : GetAuthUserId - Register Failed  : Status - " + response.StatusCode);
            }
            else
            {
                var getUserId = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
                string _userId = await getUserId.Content.ReadAsStringAsync();
                userId = _userId;
                _logger.LogDebug("AddAgentPlugin : GetAuthUserId - Register Success  : UserId - " + userId);
            }

            _logger.LogDebug("AddAgentPlugin : GetAuthUserId - End");
            return userId;
        }
    }
}