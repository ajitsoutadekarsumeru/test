using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect;

public class UserUtility : IUserUtility
{
    protected static ILogger<UserUtility> _logger;
    protected readonly IRepoFactory _repoFactory;
    private readonly ApplicationSettings _applicationSettings;
    private readonly GoogleSettings _googleSettings;
    private readonly EncryptionSettings _encryptionSettings;
    private readonly FileValidationSettings _fileValidationsettings;
    protected readonly SystemUserSettings _userSettings;

    public UserUtility(ILogger<UserUtility> logger, IRepoFactory repoFactory,
        IOptions<ApplicationSettings> applicationSettings, IOptions<GoogleSettings> googleSettings,
        IOptions<EncryptionSettings> encryptionSettings, IOptions<FileValidationSettings> fileValidationsettings, IOptions<SystemUserSettings> userSettings)
    {
        _logger = logger;
        _repoFactory = repoFactory;
        _applicationSettings = applicationSettings.Value;
        _googleSettings = googleSettings.Value;
        _encryptionSettings = encryptionSettings.Value;
        _fileValidationsettings = fileValidationsettings.Value;
        _userSettings = userSettings.Value;
    }

    public async Task<string> ValidateUserStatus(ApplicationUser appuser, dynamic dto)
    {
        string errormessage = string.Empty;
        _repoFactory.Init(dto);

        if (appuser.GetType().Name == "AgencyUser")
        {
            var agencyuser = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                        .IncludeAgencyUserWorkflow()
                                        .Where(a => a.Id == appuser.Id && !a.IsDeleted)
                                        .FirstOrDefaultAsync();
            if (agencyuser != null)
            {
                if (agencyuser.AgencyUserWorkflowState?.GetType() == typeof(AgencyUserDisabled) ||
                                       agencyuser.AgencyUserWorkflowState?.GetType() == typeof(AgencyUserRejected))
                {
                    errormessage = "Invalid user.";
                }
                else if (agencyuser.AgencyUserWorkflowState?.GetType() == typeof(AgencyUserDormant))
                {
                    errormessage = $"Account dormant. Your account is marked as dormant due to more than {_userSettings.UserInactivityDormantDays} days of inactivity. Please contact your admin or manager to reactivate your access.";
                }
                else if (agencyuser.AgencyUserWorkflowState?.GetType() == typeof(AgencyUserPendingApproval))
                {
                    errormessage = "The user is awaiting approval.";
                }
                else if (agencyuser.AuthorizationCardExpiryDate.Value.Date.CompareTo(DateTime.Now.Date) < 0)
                {
                    errormessage = "Invalid User. Authorization Card Expired.";
                }
                else if (agencyuser.AgencyUserWorkflowState?.GetType() == typeof(AgencyUserSavedAsDraft))
                {
                    errormessage = "Invalid User. The profile is not complete.";
                }
                else if (!string.IsNullOrEmpty(agencyuser.AgencyId))
                {
                    var collectionAgency = await _repoFactory.GetRepo().FindAll<Agency>()
                                            .Where(x => x.Id == agencyuser.AgencyId)
                                            .IncludeAgencyWorkflow()
                                            .FirstOrDefaultAsync();
                    if (collectionAgency.AgencyWorkflowState?.GetType() == typeof(AgencyPendingApproval))
                    {
                        errormessage = "Invalid User. User agency is awaiting approval.";
                    }
                    else if (collectionAgency.AgencyWorkflowState?.GetType() == typeof(AgencyDisabled))
                    {
                        errormessage = "Invalid User. User agency is Disabled.";
                    }
                    else if (collectionAgency.ContractExpireDate.Value.Date.CompareTo(DateTime.Now.Date) < 0)
                    {
                        errormessage = "Invalid User. User agency Contract Expired.";
                    }
                }
                else if (agencyuser.IsBlackListed)
                {
                    errormessage = "User is BlackListed.";
                }
            }
        }
        else if (appuser.GetType().Name == "CompanyUser")
        {
            var companyuser = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                        .IncludeCompanyUserWorkflow()
                                        .Where(a => a.Id == appuser.Id && !a.IsDeleted)
                                        .FirstOrDefaultAsync();
            if (companyuser != null)
            {
                if (companyuser.CompanyUserWorkflowState?.GetType() == typeof(CompanyUserDisabled) ||
                                      companyuser.CompanyUserWorkflowState?.GetType() == typeof(CompanyUserRejected))
                {
                    errormessage = "Invalid user";
                }
                else if (companyuser.CompanyUserWorkflowState?.GetType() == typeof(CompanyUserDormant))
                {
                    errormessage = $"Account dormant. Your account is marked as dormant due to more than {_userSettings.UserInactivityDormantDays} days of inactivity. Please contact your admin or manager to reactivate your access.";
                }
                else if (companyuser.CompanyUserWorkflowState?.GetType() == typeof(CompanyUserPendingApproval))
                {
                    errormessage = "The user is awaiting approval.";
                }
                else if (companyuser.CompanyUserWorkflowState?.GetType() == typeof(CompanyUserSavedAsDraft))
                {
                    errormessage = "Invalid User. The profile is not complete.";
                }
                else if (companyuser.IsDeactivated == true)
                {
                    errormessage = "User Deactivated. Please contact administrator.";
                }
            }
        }

        return errormessage;
    }

    public async Task InsertUserActivityDetails(string userid, string Activitytype, double? lat, double? lng, dynamic dto)
    {
        _repoFactory.Init(dto);
        ApplicationUser loggedInParty = new ApplicationUser();
        ApplicationUser? applicationUser = new ApplicationUser();
        CompanyUser CompanyUser = new CompanyUser();

        applicationUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(p => p.UserId == userid).FirstOrDefaultAsync();
        switch (applicationUser)
        {
            case AgencyUser agencyUser:
                await InsertAgencyUserDailyActivity(Activitytype, applicationUser.Id, lat, lng);
                break;

            case CompanyUser companyUser:
                await InsertCompanyUserDailyActivity(Activitytype, applicationUser.Id, lat, lng);
                break;

            default:
                break;
        }
    }

    private async Task InsertAgencyUserDailyActivity(string Activitytype, string Id, double? lat, double? lng)
    {
        AgencyUser? agencyUser = new AgencyUser();
        AgencyUserDesignation? agencyUserDesignation = new AgencyUserDesignation();
        AgencyUserScopeOfWork? agencyUserScopeOfWork = new AgencyUserScopeOfWork();

        agencyUser = await _repoFactory.GetRepo().Find<AgencyUser>(Id)
                                .FlexInclude(a => a.Agency)
                                .FlexInclude(x => x.AgencyUserWorkflowState)
                                .FirstOrDefaultAsync();

        agencyUserDesignation = await _repoFactory.GetRepo().FindAll<AgencyUserDesignation>()
                                            .FlexInclude(a => a.Designation)
                                            .FlexInclude(a => a.Department)
                                            .Where(x => x.AgencyUserId == agencyUser.Id)
                                            .FirstOrDefaultAsync();

        agencyUserScopeOfWork = await _repoFactory.GetRepo().FindAll<AgencyUserScopeOfWork>()
                                        .Where(x => x.AgencyUserId == agencyUser.Id)
                                        .FirstOrDefaultAsync();

        DailyActivityDetail activity = new DailyActivityDetail();
        activity.SetId(Guid.NewGuid().ToString().Replace("-", ""));
        activity.UserId = agencyUser.CustomId;
        activity.Name = agencyUser.FirstName;
        activity.Mobile = agencyUser.PrimaryMobileNumber;
        activity.EmailId = agencyUser.PrimaryEMail;
        activity.ActivityType = Activitytype;
        activity.ActivityMonth = DateTime.Now.Month;
        activity.ActivityDayNumber = DateTime.Now.Day;
        activity.ActivityYear = DateTime.Now.Year;
        activity.ActivityWeekDay = DateTime.Now.DayOfWeek.ToString();
        activity.Designation = agencyUserDesignation.Designation != null ? agencyUserDesignation.Designation.Name : "";
        activity.Branch = agencyUserScopeOfWork != null ? agencyUserScopeOfWork.Branch : string.Empty;
        activity.Department = agencyUserDesignation.Department != null ? agencyUserDesignation.Department.Name : ""; ;
        activity.ActivityTs = DateTime.Now;
        activity.StaffOrAgent = false;
        activity.EmpanalmentStatus = agencyUser.AgencyUserWorkflowState != null ? agencyUser.AgencyUserWorkflowState.Name : string.Empty;
        activity.State = agencyUserScopeOfWork != null ? agencyUserScopeOfWork.State : string.Empty;
        activity.Location = agencyUserScopeOfWork != null ? agencyUserScopeOfWork.Branch : string.Empty;
        activity.SetCreatedBy(agencyUser.Id);
        activity.SetLastModifiedBy(agencyUser.Id);
        activity.Lat = lat != null ? Convert.ToDouble(lat) : 0;
        activity.Long = lng != null ? Convert.ToDouble(lng) : 0;
        activity.Agency = agencyUser.Agency != null ? agencyUser.Agency.FirstName : "";

        if (activity != null)
        {
            activity.SetAdded();
            _repoFactory.GetRepo().InsertOrUpdate(activity);
            await _repoFactory.GetRepo().SaveAsync();
        }
    }

    private async Task InsertCompanyUserDailyActivity(string Activitytype, string Id, double? lat, double? lng)
    {
        CompanyUser? CompanyUser = new CompanyUser();
        CompanyUser = await _repoFactory.GetRepo().Find<CompanyUser>(Id)
                                .FlexInclude(x => x.CompanyUserWorkflowState)
                               .FlexInclude(x => x.Designation)
                               .FlexInclude(x => x.BaseBranch)
                               .FirstOrDefaultAsync();

        if (CompanyUser != null)
        {
            var des = await _repoFactory.GetRepo().FindAll<CompanyUserDesignation>()
                                .FlexInclude("Designation")
                                .FlexInclude("Department")
                                .Where(x => x.CompanyUserId == CompanyUser.Id && !x.IsDeleted)
                                .FirstOrDefaultAsync();

            var ScopeOfWork = await _repoFactory.GetRepo().FindAll<CompanyUserScopeOfWork>()
                                        .Where(x => x.CompanyUserId == CompanyUser.Id && !x.IsDeleted)
                                        .FirstOrDefaultAsync();

            DailyActivityDetail activity = new DailyActivityDetail();
            activity.SetId(Guid.NewGuid().ToString().Replace("-", ""));
            activity.UserId = CompanyUser.CustomId;
            activity.Name = CompanyUser.FirstName;
            activity.Mobile = CompanyUser.PrimaryMobileNumber;
            activity.EmailId = CompanyUser.PrimaryEMail;
            activity.ActivityType = Activitytype;
            activity.ActivityMonth = DateTime.Now.Month;
            activity.ActivityDayNumber = DateTime.Now.Day;
            activity.ActivityYear = DateTime.Now.Year;
            activity.ActivityWeekDay = DateTime.Now.DayOfWeek.ToString();
            activity.Designation = des != null ? des.Designation.Name : string.Empty;
            activity.Branch = CompanyUser.BaseBranch != null ? CompanyUser.BaseBranch.FirstName : string.Empty;
            activity.Department = des != null ? des.Department.Code : string.Empty;
            activity.ActivityTs = DateTime.Now;
            activity.StaffOrAgent = true;
            activity.EmpanalmentStatus = CompanyUser.CompanyUserWorkflowState != null ? CompanyUser.CompanyUserWorkflowState.Name : string.Empty;
            activity.State = ScopeOfWork != null ? ScopeOfWork.State : string.Empty;
            activity.Location = ScopeOfWork != null ? ScopeOfWork.Location : string.Empty;
            activity.SetCreatedBy(CompanyUser.Id);
            activity.SetLastModifiedBy(CompanyUser.Id);
            activity.Lat = lat != null ? Convert.ToDouble(lat) : 0;
            activity.Long = lng != null ? Convert.ToDouble(lng) : 0;

            if (activity != null)
            {
                activity.SetAdded();
                _repoFactory.GetRepo().InsertOrUpdate(activity);
                await _repoFactory.GetRepo().SaveAsync();
            }
        }
    }

    public async Task<List<Permissions>> GetUserPermissions(ApplicationUser appUser, DtoBridge dto)
    {
        // Initialize repository
        _repoFactory.Init(dto);
        var repo = _repoFactory.GetRepo();

        // Fetch user's designations based on their type
        var designations = appUser switch
        {
            AgencyUser => await repo.FindAll<AgencyUserDesignation>()
                                    .Where(x => x.AgencyUserId == appUser.Id && !x.IsDeleted)
                                    .Select(x => x.Designation)
                                    .ToListAsync(),

            CompanyUser => await repo.FindAll<CompanyUserDesignation>()
                                     .Where(x => x.CompanyUserId == appUser.Id && !x.IsDeleted)
                                     .Select(x => x.Designation)
                                     .ToListAsync(),

            _ => new List<Designation>()
        };

        if (!designations.Any())
            return new List<Permissions>(); // No designations, hence no permissions

        // Get Designation IDs
        var designationIds = designations.Select(d => d.Id).ToList();

        // Fetch associated permission scheme IDs from Designations
        var permissionSchemeIds = await repo.FindAll<Designation>()
                                            .Where(x => designationIds.Contains(x.Id) && !x.IsDeleted)
                                            .Select(x => x.PermissionSchemeId)
                                            .ToListAsync();

        if (!permissionSchemeIds.Any())
            return new List<Permissions>(); // No permission schemes found

        // Fetch distinct permissions from enabled permissions under those schemes
        var permissions = await repo.FindAll<EnabledPermission>()
                                    .Where(ep => permissionSchemeIds.Contains(ep.PermissionSchemeId))
                                    .Select(ep => ep.Permission)
                                    .Distinct()
                                    .ToListAsync();

        return permissions;
    }

    public async Task<Dictionary<string, string>> GetClaims(DtoBridge dto)
    {
        // Initialize repository
        _repoFactory.Init(dto);
        var repo = _repoFactory.GetRepo();

        var hierarchyLevels = await repo.FindAll<HierarchyLevel>().Select(x => new { x.Id, x.Name, x.Order, Type = x.Type.ToLower() }).ToListAsync();
        var productScope = hierarchyLevels.Where(x => x.Type == "product").Select(x => new { x.Id, x.Name, x.Order }).ToList();
        var geoScope = hierarchyLevels.Where(x => x.Type == "geo").Select(x => new { x.Id, x.Name, x.Order }).ToList();
        var hierarchy = new { Product = productScope, Geo = geoScope };

        var claims = new Dictionary<string, string>();

        //Add all system settings
        claims.Add("CurrencySymbol", _applicationSettings.CurrencySymbol);
        claims.Add("CurrencyCode", _applicationSettings.CurrencyCode);
        claims.Add("WalletLimit", _applicationSettings.WalletLimit.ToString());
        claims.Add("SKey", _encryptionSettings.StaticKeys.EncryptionKey.ToString());
        claims.Add("GKey", _googleSettings.EncryptedAPIKey.ToString());
        claims.Add("MaxNumberOfFiles", _fileValidationsettings.MaxNumberOfFiles.ToString());
        claims.Add("MaximumFileSizeInMb", _fileValidationsettings.MaxDownloadFileSizeMb.ToString());
        
        claims.Add("Hierarchy", JsonSerializer.Serialize(hierarchy));

        return claims;
    }
}