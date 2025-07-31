using Babel.Licensing;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ENTiger.ENCollect
{
    public class LicenseService : ILicenseService
    {
        public LicenseInfo License { get; private set; }
        private readonly byte[] _secretKeyBytes;
        private readonly ILogger<LicenseService> _logger;
        private readonly IFileSystem _fileSystem;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        private readonly LicenseColorSettings _licenseColorSettings;
        // Implementation that loads, verifies, and exposes the license
        public LicenseService(ILogger<LicenseService> logger, IFlexHost flexHost, IConfiguration configuration, IRepoFactory repoFactory, IFileSystem fileSystem, IOptions<LicenseInfo> license, IOptions<LicenseColorSettings> licenseColorSettings)
        {
            _repoFactory = repoFactory;
            _flexHost = flexHost;
            _logger = logger;
            _fileSystem = fileSystem;
            _licenseColorSettings = licenseColorSettings.Value;
            //License = license.Value;

            var _licenseFilePath = _fileSystem.Path.Combine(Directory.GetCurrentDirectory(), configuration["License:Path"]);

            if (!File.Exists(_licenseFilePath))
            {
                _logger.LogError("License file not found.");
                throw new InvalidOperationException("Valid license not available for application usage.");
            }

            var json = File.ReadAllText(_licenseFilePath);
            License = JsonSerializer.Deserialize<LicenseInfo>(json) ?? throw new InvalidOperationException("Malformed license file.");

            var secretBase64 = configuration["License:SecretKey"]
                ?? throw new InvalidOperationException("License secret key not configured.");
            _secretKeyBytes = Convert.FromBase64String(secretBase64);

            if (!IsLicenseValid())
                throw new InvalidOperationException("License is invalid or expired.");

        }

        public bool IsLicenseValid()
        {
            // Check date validity
            if (License.ExpiresOn <= DateTime.UtcNow)
                return false;

            // Verify signature
            return VerifySignature();
        }

        private bool VerifySignature()
        {
            // Build a sorted dictionary to ensure consistent ordering
            var payload = new SortedDictionary<string, object>
            {
                ["ExpiresOn"] = License.ExpiresOn,
                ["FreeLimits"] = new Dictionary<string, int>
                {
                    ["Users"] = License.FreeLimits.Users,
                    ["Collections"] = License.FreeLimits.Collections,
                    ["Trails"] = License.FreeLimits.Trails
                },
                ["IssuedOn"] = License.IssuedOn,
                ["IssuedTo"] = License.IssuedTo,
                ["LicenseCounts"] = new Dictionary<string, int>
                {
                    ["Field"] = License.LicenseCounts.Field,
                    ["Tele"] = License.LicenseCounts.Tele
                },
                ["LicenseKey"] = License.LicenseKey
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            // Serialize the sorted dictionary to UTF-8 bytes
            byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(payload, options);

            // (Optional) Debug: compare these bytes to your Python-generated message
            // Console.WriteLine(Encoding.UTF8.GetString(jsonBytes));

            using var hmac = new HMACSHA256(_secretKeyBytes);
            var computedHash = hmac.ComputeHash(jsonBytes);
            var computedSignature = Convert.ToBase64String(computedHash);

            return computedSignature == License.Signature;
        }

        public async Task<int> GetFieldLicenseConsumptionAsync(dynamic dto)
        {
            _repoFactory.Init(dto);
            AgencyUserWorkflowState agentUserState = WorkflowStateFactory.GetCollectionAgencyUserWFState("approved");
            CompanyUserWorkflowState companyUserState = WorkflowStateFactory.GetCompanyUserWFState("approved");
            int agencyUsersNo = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                               .IncludeAgencyUserWorkflow()
                                               .ByAgencyUserType(UserTypeEnum.FOS.ToString())
                                               .ByWorkflowState(agentUserState)
                                               .CountAsync();
            int companyUsersNo = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                               .IncludeCompanyUserWorkflow()
                                               .ByCompanyUserType(UserTypeEnum.FOS.ToString())
                                               .ByCompanyUserWorkFlowState(companyUserState.Name)
                                               .CountAsync();

            return agencyUsersNo + companyUsersNo;
        }

        public async Task<int> GetTeleLicenseConsumptionAsync(dynamic dto)
        {
            _repoFactory.Init(dto);
            AgencyUserWorkflowState agentUserState = WorkflowStateFactory.GetCollectionAgencyUserWFState("approved");
            CompanyUserWorkflowState companyUserState = WorkflowStateFactory.GetCompanyUserWFState("approved");
            int agencyUsersNo = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                               .IncludeAgencyUserWorkflow()
                                               .ByAgencyUserType(UserTypeEnum.Telecaller.ToString())
                                               .ByWorkflowState(agentUserState)
                                               .CountAsync();
            int companyUsersNo = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                               .IncludeCompanyUserWorkflow()
                                               .ByCompanyUserType(UserTypeEnum.Telecaller.ToString())
                                               .ByCompanyUserWorkFlowState(companyUserState.Name)
                                               .CountAsync();

            return agencyUsersNo + companyUsersNo;
        }

        public async Task<int> GetFreeLicenseConsumptionAsync(dynamic dto)
        {
            _repoFactory.Init(dto);
            AgencyUserWorkflowState agentUserState = WorkflowStateFactory.GetCollectionAgencyUserWFState("approved");
            CompanyUserWorkflowState companyUserState = WorkflowStateFactory.GetCompanyUserWFState("approved");
            int agencyUsersNo = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                               .IncludeAgencyUserWorkflow()
                                               .ByAgencyUserType(UserTypeEnum.Others.ToString())
                                               .ByWorkflowState(agentUserState)
                                               .CountAsync();
            int companyUsersNo = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                               .IncludeCompanyUserWorkflow()
                                               .ByCompanyUserType(UserTypeEnum.Others.ToString())
                                               .ByCompanyUserWorkFlowState(companyUserState.Name)
                                               .CountAsync();

            return agencyUsersNo + companyUsersNo;
        }

        public async Task<int> GetFreeCollectionsConsumptionAsync(string userId, dynamic dto)
        {
            _repoFactory.Init(dto);
            return await _repoFactory.GetRepo().FindAll<Collection>()
                .ByCollector(userId)
                .ByCollectionsForMonth(DateTime.Today)
                .CountAsync();
        }

        public async Task<int> GetFreeTrailsConsumptionAsync(string userId, dynamic dto)
        {
            _repoFactory.Init(dto);
            return await _repoFactory.GetRepo().FindAll<Feedback>()
                        .ByFeedbackCollector(userId)
                        .ByFeedbackForMonth(DateTime.Today)
                        .CountAsync();
        }

        public int GetFieldLicenseLimit()
        {
            return License.LicenseCounts.Field;
        }

        public int GetTeleLicenseLimit()
        {
            return License.LicenseCounts.Tele;
        }

        public int GetFreeLicenseLimit()
        {
            return License.FreeLimits.Users;
        }

        public int GetFreeCollectionsLicenseLimit()
        {
            return License.FreeLimits.Collections;
        }

        public int GetFreeTrailsLicenseLimit()
        {
            return License.FreeLimits.Trails;
        }

        public async Task LogLicenseViolationAsync(LicenseFeatureEnum feature, int limit, int consumption, FlexAppContextBridge hostContextInfo)
        {
            var violation = new LicenseViolation
            {
                Feature = feature.DisplayName,
                Limit = limit,
                Actual = consumption,
                ErrorMessage = $"{feature.DisplayName} usage {consumption} exceeds limit {limit}."
            };

            _repoFactory.Init(hostContextInfo);

            var record = _flexHost.GetDomainModel<LicenseViolation>().Create(violation, hostContextInfo.UserId);

            _repoFactory.GetRepo().InsertOrUpdate(record);
            int records = await _repoFactory.GetRepo().SaveAsync();

            if (records > 0)
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(Object).Name, record.Id);
            else
                _logger.LogWarning("No records inserted for LogViolationAsync");
        }

        public async Task<LicenseValidationResult> ValidateUserLicenseLimitAsync(UserTypeEnum userType, dynamic Dto)
        {
            LicenseValidationResult result = new LicenseValidationResult();
            result.UserType = userType;
            int consumption = 0;
            int limit = 0;

            switch (userType)
            {
                case UserTypeEnum.FOS:
                    consumption = await GetFieldLicenseConsumptionAsync(Dto);
                    limit = GetFieldLicenseLimit();
                    break;
                case UserTypeEnum.Telecaller:
                    consumption = await GetTeleLicenseConsumptionAsync(Dto);
                    limit = GetTeleLicenseLimit();
                    break;
                case UserTypeEnum.Others:
                    consumption = await GetFreeLicenseConsumptionAsync(Dto);
                    limit = GetFreeLicenseLimit();                
                    break;
            }
            result.PermittedCount = limit;
            result.ActualCount = consumption;
            result.Valid = !(consumption >= limit);
            return result;
        }

        public async Task<LicenseValidationTransactionsResult> ValidateTransactionLicenseLimitAsync(LicenseTransactionType transactionType, string userId, dynamic Dto)
        {
            LicenseValidationTransactionsResult result = new LicenseValidationTransactionsResult();
            result.TransactionType = transactionType;
            int consumption = 0;
            int limit = 0;

            switch (transactionType)
            {
                case LicenseTransactionType.Collections:
                    consumption = await GetFreeCollectionsConsumptionAsync(userId, Dto);
                    limit = GetFreeCollectionsLicenseLimit();
                    break;
                case LicenseTransactionType.Trails:
                    consumption = await GetFreeTrailsConsumptionAsync(userId, Dto);
                    limit = GetFreeTrailsLicenseLimit();
                    break;
            }
            result.PermittedCount = limit;
            result.ActualCount = consumption;
            result.Valid = !(consumption >= limit);
            return result;
        }

        public async Task<GetUserTransactionLimitsDto> GetTransactionTypeDetailAsync(LicenseTransactionType transactionType, string userId, dynamic dto)
        {
            GetUserTransactionLimitsDto userTypeDetails = new();
            var licenseValidationResult = await ValidateTransactionLicenseLimitAsync(transactionType, userId, dto);
            userTypeDetails = new GetUserTransactionLimitsDto();
            userTypeDetails.Limit = licenseValidationResult.PermittedCount;
            userTypeDetails.CurrentConsumption = licenseValidationResult.ActualCount;
            userTypeDetails.TransactionType = transactionType.ToString();
            decimal used = (userTypeDetails.CurrentConsumption / (userTypeDetails.Limit == 0 ? 1 : userTypeDetails.Limit)) * 100;
            userTypeDetails.PercentUsed = Math.Floor(used);
            userTypeDetails.ColourCode = await GetUtilizationColor(userTypeDetails.PercentUsed);
            return userTypeDetails;
        }

        public async Task<GetUserTypeDetailsDto> GetUserTypeDetailAsync(UserTypeEnum userType, dynamic dto)
        {
            GetUserTypeDetailsDto userTypeDetails = new();
            var licenseValidationResult = await ValidateUserLicenseLimitAsync(userType, dto);
            userTypeDetails = new GetUserTypeDetailsDto();
            userTypeDetails.Limit = licenseValidationResult.PermittedCount;
            userTypeDetails.CurrentConsumption = licenseValidationResult.ActualCount;
            userTypeDetails.UserType = userType.ToString();
            decimal used = (userTypeDetails.CurrentConsumption / (userTypeDetails.Limit == 0 ? 1 : userTypeDetails.Limit)) * 100;
            userTypeDetails.PercentUsed = Math.Floor(used);
            userTypeDetails.ColourCode = await GetUtilizationColor(userTypeDetails.PercentUsed);
            return userTypeDetails;
        }
        public async Task<decimal> GetUserTypeLimitPercentageDetailAsync(UserTypeEnum userType, dynamic dto)
        {
            GetUserTypeDetailsDto userTypeDetails = new();
            var licenseValidationResult = await ValidateUserLicenseLimitAsync(userType, dto);
            decimal limit = licenseValidationResult.PermittedCount == 0 ? 1 : licenseValidationResult.PermittedCount;
            decimal used = (licenseValidationResult.ActualCount / limit) * 100;
            used = Math.Floor(used);
            return used;
        }

        public async Task<string> GetUtilizationColor(decimal utilizedPercentage)
        {

            if (utilizedPercentage < _licenseColorSettings.GreenThreshold)
                return RAGColorEnum.Green.Value;

            if (utilizedPercentage < _licenseColorSettings.AmberThreshold)
                return RAGColorEnum.Amber.Value;

            return RAGColorEnum.Red.Value;
        }
    }

    public class LicenseInfo
    {
        /// <summary>Unique license identifier.</summary>
        public string LicenseKey { get; set; } = string.Empty;

        /// <summary>Entity or customer to whom the license is issued.</summary>
        public string IssuedTo { get; set; } = string.Empty;

        /// <summary>UTC timestamp when the license was issued.</summary>
        public DateTime IssuedOn { get; set; } = DateTime.Now;

        /// <summary>UTC timestamp when the license expires.</summary>
        public DateTime ExpiresOn { get; set; } = DateTime.Now;

        /// <summary>Counts for licensed features (e.g., Agents, Telecallers).</summary>
        public LicenseCounts LicenseCounts { get; set; } = new LicenseCounts();

        /// <summary>Free-tier limits (e.g., Users, Collections, Trails).</summary>
        public FreeLimits FreeLimits { get; set; } = new FreeLimits();

        /// <summary>HMAC-SHA256 signature over the canonical payload.</summary>
        public string Signature { get; set; } = string.Empty;
    }

    public class LicenseCounts
    {
        public int Field { get; set; } = 50;
        public int Tele { get; set; } = 50;
    }

    public class FreeLimits
    {
        public int Users { get; set; } = 200;
        public int Collections { get; set; } = 10;
        public int Trails { get; set; } = 50;
    }

    public class LicenseValidationResult
    {
        public UserTypeEnum UserType { get; set; }
        public int PermittedCount { get; set; }
        public int ActualCount { get; set; }
        public bool Valid { get; set; }
    }

    public class LicenseValidationTransactionsResult
    {
        public LicenseTransactionType TransactionType { get; set; }
        public int PermittedCount { get; set; }
        public int ActualCount { get; set; }
        public bool Valid { get; set; }
    }
}
