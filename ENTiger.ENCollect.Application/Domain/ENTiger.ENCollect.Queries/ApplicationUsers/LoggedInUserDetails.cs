using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class LoggedInUserDetails : FlexiQueryBridgeAsync<ApplicationUser, LoggedInUserDetailsDto>
    {
        protected readonly ILogger<LoggedInUserDetails> _logger;
        protected LoggedInUserDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly AuthorizationCardSettings _authorizationCardNotificationSettings;

        protected string id = string.Empty;
        private int _cardExpiryDateCheckDays = 0;
        protected string _lastSuccessLoginMessage = string.Empty;
        protected string _lastFailLoginMessage = string.Empty;

        private List<Department> departments;
        private List<Designation> designations;
        private List<Accountability> Accountability;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public LoggedInUserDetails(ILogger<LoggedInUserDetails> logger, IRepoFactory repoFactory, IOptions<AuthorizationCardSettings> authorizationCardNotificationSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authorizationCardNotificationSettings = authorizationCardNotificationSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual LoggedInUserDetails AssignParameters(LoggedInUserDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<LoggedInUserDetailsDto> Fetch()
        {
            _cardExpiryDateCheckDays = _authorizationCardNotificationSettings.ExpiryNotificationInDays;
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            id = _flexAppContext.UserId;
            string userType = string.Empty;
            LoggedInUserDetailsDto result = new LoggedInUserDetailsDto();
            var user = await Build<ApplicationUser>().IncludeUserProductScope()
                        .IncludeUserGeoScope().IncludeUserBucketScope().FirstOrDefaultAsync();
                        
            if (user != null)
            {
                departments = await _repoFactory.GetRepo().FindAll<Department>().ToListAsync();

                designations = await _repoFactory.GetRepo().FindAll<Designation>().ToListAsync();

                Accountability = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                                .Where(x => x.ResponsibleId == id)
                                                                .FlexInclude(x => x.AccountabilityType)
                                                                .OrderByDescending(x => x.CreatedDate)
                                                                .ToListAsync();

                if (user.GetType() == typeof(CompanyUser))
                {
                    result = await FetchCompanyUser(user.Id);
                    userType = "Staff";
                }
                if (user.GetType() == typeof(AgencyUser))
                {
                    result = await FetchAgent(user.Id);
                    userType = "AgencyUser";
                }

                if (result != null)
                {
                    result.UserType = userType;
                    result.ProductLevelId = user.ProductLevelId;
                    result.GeoLevelId = user.GeoLevelId;
                    result.ProductScopes = user.ProductScopes?.Select(x => new UserProductScopeDto { ProductScopeId = x.ProductScopeId}).ToList();
                    result.GeoScopes = user.GeoScopes?.Select(x => new UserGeoScopeDto { GeoScopeId = x.GeoScopeId }).ToList();
                    result.BucketScopes = user.BucketScopes?.Select(x => new UserBucketScopeDto { BucketScopeId = x.BucketScopeId }).ToList();

                    _logger.LogInformation("Lastsucessdata fetch started");
                    LoginDetailsHistory? lastSuccessData = await _repoFactory.GetRepo().FindAll<LoginDetailsHistory>()
                                                            .Where(a => a.UserId == id && a.LoginStatus == LoginStatusEnum.Success.Value)
                                                            .OrderByDescending(s => s.CreatedDate).FirstOrDefaultAsync();

                    if (lastSuccessData != null)
                    {
                        result.LastSuccessLoginMessage = "Last Successful Login :" + " " + lastSuccessData.CreatedDate.DateTime + ".";
                    }
                    _logger.LogInformation("LastFaildata fetch started");
                    LoginDetailsHistory? lastFailData = await _repoFactory.GetRepo().FindAll<LoginDetailsHistory>()
                                                        .Where(a => a.UserId == id && a.LoginStatus == LoginStatusEnum.Fail.Value)
                                                        .OrderByDescending(s => s.CreatedDate).FirstOrDefaultAsync();

                    if (lastFailData != null)
                    {
                        result.LastFailLoginMessage = $"Last Failed Login: {lastFailData.CreatedDate.DateTime}.";
                    }                    
                }
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(x => x.Id == id);

            //Build Your Query Here

            return query;
        }

        private async Task<LoggedInUserDetailsDto> FetchCompanyUser(string id)
        {
            LoggedInUserDetailsDto user = new LoggedInUserDetailsDto();
            CompanyUser? companyUser = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                            .Where(x => x.Id == id)
                            .IncludeBaseBranch()
                            .IncludeDesignation()
                            .IncludeCompanyUserWorkflow()
                            .FirstOrDefaultAsync();

            if (companyUser != null)
            {
                user.Id = companyUser.Id;
                user.CustomId = companyUser.CustomId;
                user.EmployeeId = companyUser.EmployeeId;
                user.FirstName = companyUser.FirstName;
                user.MiddleName = companyUser.MiddleName;
                user.LastName = companyUser.LastName;
                user.PrimaryEMail = companyUser.PrimaryEMail;
                user.PrimaryMobileNumber = companyUser.PrimaryMobileNumber;
                user.DateOfBirth = companyUser.DateOfBirth;
                user.ProfileImage = companyUser.ProfileImage;
                user.BaseBranchId = companyUser.BaseBranchId;
                user.BaseBranchFirstName = companyUser.BaseBranch.FirstName;
                user.BaseBranchLastName = companyUser.BaseBranch.LastName;
                user.ValidFrom = companyUser.CreatedDate.Date;
                user.Status = companyUser.CompanyUserWorkflowState.Name;
                user.SecondaryContactNumber = companyUser.SecondaryContactNumber;
                user.IsPolicyAccepted = companyUser.IsPolicyAccepted;
                user.PolicyAcceptedDate = companyUser.PolicyAcceptedDate;
                user.BloodGroup = companyUser.BloodGroup;
                user.EmergencyContactNo = companyUser.EmergencyContactNo;
                user.ResponsibilityInfos = Accountability.Select(a => new ResponsibilityInfoDto
                {
                    Id = a.ResponsibleId,
                    Role = a.AccountabilityTypeId,
                    Description = a.AccountabilityType.Description

                }).ToList();
                user.Roles = companyUser.Designation.Select(d => new UserDesignationDto
                {
                    Id = d.Id,
                    DepartmentId = d.Department.Name,
                    DesignationId = d.Designation.Name,
                    IsPrimaryDesignation = d.IsPrimaryDesignation,
                    Role = "BankTo"
                            + departments.Where(w => w.Id == d.DepartmentId).Select(s => s.DepartmentTypeId).FirstOrDefault()
                            + designations.Where(w => w.Id == d.DesignationId).Select(s => s.DesignationTypeId).FirstOrDefault()
                }).ToList();
            }

            return user;
        }

        private async Task<LoggedInUserDetailsDto> FetchAgent(string id)
        {
            LoggedInUserDetailsDto user = new LoggedInUserDetailsDto();
            AgencyUser? agenyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                            .Where(x => x.Id == id)
                            .IncludeAgencyUserDesignation()
                            .IncludeAgencyUserDepartmentName()
                            .IncludeAgencyUserDesignationName()
                            .IncludeAgencyUserAgency()
                            .IncludeAgencyUserAgencyAddress()
                            .IncludeAgencyUserAddress()
                            .IncludeAgencyUserWorkflow()
                            .FirstOrDefaultAsync();

            if (agenyUser != null)
            {
                user.Id = agenyUser.Id;
                user.CustomId = agenyUser.CustomId;
                user.FirstName = agenyUser.FirstName;
                user.MiddleName = agenyUser.MiddleName;
                user.LastName = agenyUser.LastName;
                user.PrimaryEMail = agenyUser.PrimaryEMail;
                user.PrimaryMobileNumber = agenyUser.PrimaryMobileNumber;
                user.DateOfBirth = agenyUser.DateOfBirth;
                user.City = agenyUser.Address != null ? agenyUser.Address.City : string.Empty;
                user.ProfileImage = agenyUser.ProfileImage;
                user.ValidTo = agenyUser.AuthorizationCardExpiryDate;
                user.ValidFrom = agenyUser.CreatedDate.Date;
                user.Status = agenyUser.AgencyUserWorkflowState.Name;
                user.SecondaryContactNumber = agenyUser.SecondaryContactNumber;
                user.BloodGroup = agenyUser.BloodGroup;
                user.EmergencyContactNo = agenyUser.EmergencyContactNo;
                user.IdCardNumber = agenyUser.IdCardNumber;
                user.IsPolicyAccepted = agenyUser.IsPolicyAccepted;
                user.PolicyAcceptedDate = agenyUser.PolicyAcceptedDate;
                if (agenyUser.Agency != null)
                {
                    user.AgencyId = agenyUser.Agency.Id;
                    user.AgencyCode = agenyUser.Agency.CustomId;
                    user.CustomId = agenyUser.CustomId;
                    user.AgencyFirstName = agenyUser.Agency.FirstName;
                    user.AgencyLastName = agenyUser.Agency.LastName;
                    if (agenyUser.Agency.Address != null)
                    {
                        user.AgencyAddress = agenyUser.Agency?.Address?.AddressLine + " " + agenyUser.Agency?.Address?.State + " " + agenyUser.Agency?.Address?.City + " " + agenyUser.Agency?.Address?.PIN;
                    }
                }
                user.AuthorizationCardDateExpiryMessage = await CheckAuthorizationCardExpiryDate(agenyUser.Id, agenyUser.AuthorizationCardExpiryDate, agenyUser.FirstName);
                user.ResponsibilityInfos = Accountability.Select(a => new ResponsibilityInfoDto
                {
                    Id = a.ResponsibleId,
                    Role = a.AccountabilityTypeId,
                    Description = a.AccountabilityType.Description

                }).ToList();

                user.Roles = agenyUser.Designation?.Select(d => new UserDesignationDto
                {
                    Id = d.Id,
                    DepartmentId = d.Department.Name,
                    DesignationId = d.Designation.Name,
                    IsPrimaryDesignation = d.IsPrimaryDesignation,
                    Role = "AgencyTo"
                                + departments.Where(w => w.Id == d.DepartmentId).Select(s => s.DepartmentTypeId).FirstOrDefault()
                                + designations.Where(w => w.Id == d.DesignationId).Select(s => s.DesignationTypeId).FirstOrDefault()
                }).ToList();
            }

            return user;
        }

        private async Task<string> CheckAuthorizationCardExpiryDate(string userId, DateTime? authorizationExpiryDate, string firstName)
        {
            if (!authorizationExpiryDate.HasValue)
            {
                return "";
            }
            DateTime currentDate = DateTime.Now.Date;
            DateTime startDate = currentDate;
            DateTime endDate = startDate.AddDays(1);

            _logger.LogInformation("LoggedInUserDetails : fetch authorizationExpiryDate" + authorizationExpiryDate+ " for user "+firstName );


            int diffDays = (authorizationExpiryDate.Value - currentDate).Days;
            _logger.LogInformation("LoggedInUserDetails : diffrence days for AuthorizationExpiryDays" + diffDays);

            if (diffDays <= Convert.ToInt32(_cardExpiryDateCheckDays))
            {
                var userAttendanceDetailExist = await _repoFactory.GetRepo().FindAll<UserAttendanceLog>()
                    .Where(p => p.ApplicationUserId == userId && p.CreatedDate >= startDate && p.CreatedDate < endDate).ToListAsync();

                _logger.LogInformation("Login count for the day for this user: " +  userAttendanceDetailExist.Count);

                if (userAttendanceDetailExist.Count == 1)
                {
                    return "You authorization card is about to expire, please contact administrator.";
                }
            }
            return "";
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class LoggedInUserDetailsParams : DtoBridge
    {
    }
}