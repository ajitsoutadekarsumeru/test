using Elastic.Clients.Elasticsearch.Security;
using Elastic.Clients.Elasticsearch.TransformManagement;
using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class AddCompanyUserPlugin : FlexiPluginBase, IFlexiPlugin<AddCompanyUserPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12cbc619db8a5f9a0543050ee3a0ab";
        public override string FriendlyName { get; set; } = "AddCompanyUserPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddCompanyUserPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;
        private readonly ILicenseService _licenseService;

        protected decimal _userLimitPercentageUsed;
        protected CompanyUser? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        private string authUrl = string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly LicenseSettings _licenseSettings;


        private readonly IApiHelper _apiHelper;
        protected AuditEventData _auditData;
            public AddCompanyUserPlugin(ILogger<AddCompanyUserPlugin> logger,
                IFlexHost flexHost,
                IRepoFactory repoFactory,
                IFlexPrimaryKeyGeneratorBridge pkGenerator,
                IOptions<AuthSettings> authSettings,
                ICustomUtility customUtility,
                IApiHelper apiHelper,
                ILicenseService licenseService, IOptions<LicenseSettings> loginSettings)
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

            public virtual async Task Execute(AddCompanyUserPostBusDataPacket packet)
            {
                authUrl = _authSettings.AuthUrl;
                _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
                _repoFactory.Init(packet.Cmd.Dto);

                _model = _flexHost.GetDomainModel<CompanyUser>().AddCompanyUser(packet.Cmd);

                string tenantId = _flexAppContext.TenantId;
                string userName = await GetUserNameAsync(_model, tenantId);
                var userId = await GetUserIdAsync(userName, _model.FirstName, tenantId);
                _model.SetUserId(userId);

                string customId = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.CompanyUser.Value);
                _model.SetCustomId(customId);

                List<Accountability> accountabilities = new List<Accountability>();
                accountabilities = await GetAccountabilitiesAsync(_model);

                _model.TransactionSource = _flexAppContext?.RequestSource;
                _repoFactory.GetRepo().InsertOrUpdate(_model);
                foreach (Accountability accountability in accountabilities)
                {
                    _repoFactory.GetRepo().InsertOrUpdate(accountability);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(CompanyUser).Name, _model.Id);

                    await GenerateAndSendAuditEventAsync(packet);

                    UserTypeEnum userType = Enum.TryParse(_model.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
                    _userLimitPercentageUsed = await _licenseService.GetUserTypeLimitPercentageDetailAsync(userType,packet.Cmd.Dto);
                }
                else
                {
                    _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(CompanyUser).Name, _model.Id);
                }
                EventCondition = CONDITION_ONSUCCESS;

                await this.Fire(EventCondition, packet.FlexServiceBusContext);
            }

        private async Task GenerateAndSendAuditEventAsync(AddCompanyUserPostBusDataPacket packet)
        {
            //for recurring loop use this settings
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string jsonPatch = JsonConvert.SerializeObject(_model, settings);

            _auditData = new AuditEventData(
                                EntityId: _model?.Id,
                                EntityType: AuditedEntityTypeEnum.Staff.Value,
                                Operation: AuditOperationEnum.Add.Value,
                                JsonPatch: jsonPatch,
                                InitiatorId: _flexAppContext?.UserId,
                                TenantId: _flexAppContext?.TenantId,
                                ClientIP: _flexAppContext?.ClientIP
                            );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<string> GetUserNameAsync(CompanyUser companyUser, string tenantId)
        {
            string userName = tenantId + "_" + companyUser.PrimaryEMail;
            FeatureMaster feature = _flexHost.GetDomainModel<FeatureMaster>();

            feature = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(x => string.Equals(x.Parameter, "AD")).FirstOrDefaultAsync();

            if (feature != null)
            {
                _logger.LogInformation("AddCompanyUserFFPlugin : Create Active Directory Auth User");
                //companyUser.UserId = GetDomainUserIdAsync(companyUser.DomainId, model.FirstName);
                userName = companyUser.DomainId;
            }
            return userName;
        }

        private async Task<string> GetUserIdAsync(string userName, string name, string tenantId)
        {
            _logger.LogInformation("AddCompanyUserFFPlugin : GetUserIdAsync - Start | UserName - " + userName + " | Name - " + name);
            string userId = "";

            string authapiUrl = authUrl;

            _logger.LogInformation("AddCompanyUserFFPlugin : GetUserIdAsync - AuthUrl : " + authapiUrl);
                        
            var data = new
            {
                email = userName,
                password = _authSettings.Password,
                confirmPassword = _authSettings.ConfirmPassword,
                firstName = name,
                middleName = "",
                lastName = ""
            };
            string jsonPayload = JsonConvert.SerializeObject(data);

            _logger.LogInformation("AddCompanyUserFFPlugin : GetUserIdAsync - Auth JSON Data : " + jsonPayload);
            
            var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
            string authUserId = await checkExistName.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(authUserId))
            {
                string appId;

                appId = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(p => p.UserId == authUserId).Select(q => q.Id).FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(appId))
                {
                    var dataDelete = new
                    {
                        id = authUserId,
                        email = userName
                    };

                    string DeletejsonPayload = JsonConvert.SerializeObject(dataDelete);
                    var deleteExistEmailName = await _apiHelper.SendRequestAsync(DeletejsonPayload, authapiUrl + "/api/AccountAPI/DeleteExistEmailName", HttpMethod.Post);
                }
                userId = authUserId;
            }
            System.Diagnostics.Trace.WriteLine("register user");
            var response = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/Register", HttpMethod.Post);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("AddCompanyUserFFPlugin: GetUserIdAsync - Auth Response : " + response);
            }
            else
            {
                var getUserId = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
                string _userId = await getUserId.Content.ReadAsStringAsync();
                userId = _userId;
            }
            _logger.LogInformation("AddCompanyUserFFPlugin : GetUserIdAsync - End  | Auth UserId - " + userId);
            return userId;
        }

        private async Task<List<Accountability>> GetAccountabilitiesAsync(CompanyUser model)
        {
            _logger.LogInformation("AddCompanyUserFFPlugin : GetAccountabilities - Start");
            string accountabilityTypeId = string.Empty;
            List<string> accountabilityTypeList = new List<string>();
            List<Accountability> accountabilities = new List<Accountability>();
            if (model.Designation != null && model.Designation.Count > 0)
            {
                _logger.LogInformation("AddCompanyUserFFPlugin : Roles Count - " + model.Designation.Count);
                foreach (var role in model.Designation)
                {
                    _logger.LogInformation("AddCompanyUserFFPlugin : DepartmentId - " + role.DepartmentId + " | DesignationId - " + role.DesignationId);
                    var departmentTypeID = await _repoFactory.GetRepo().FindAll<Department>().Where(d => d.Id == role.DepartmentId).Select(a => a.DepartmentTypeId).FirstOrDefaultAsync();

                    var designationtypeid = await _repoFactory.GetRepo().FindAll<Designation>().Where(x => x.Id == role.DesignationId).Select(a => a.DesignationTypeId).FirstOrDefaultAsync();

                    accountabilityTypeId = "BankTo" + departmentTypeID + designationtypeid;
                    _logger.LogInformation("AddCompanyUserFFPlugin : AccountabilityTypeId - " + accountabilityTypeId);
                    if (!accountabilityTypeList.Contains(accountabilityTypeId))
                    {
                        accountabilityTypeList.Add(accountabilityTypeId);
                        Accountability accountability = new Accountability();
                        accountability.CommisionerId = model.BaseBranchId;
                        accountability.ResponsibleId = model.Id;
                        accountability.AccountabilityTypeId = accountabilityTypeId;
                        accountability.SetAddedOrModified();
                        accountability.SetCreatedBy(model.CreatedBy);
                        accountabilities.Add(accountability);
                    }
                }
            }
            _logger.LogInformation("AddCompanyUserFFPlugin : GetAccountabilities - End");
            return accountabilities;
        }
       
    }
}