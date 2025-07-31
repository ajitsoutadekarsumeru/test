using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUser : ApplicationUser
    {
        protected readonly ILogger<CompanyUser> _logger;
        protected readonly IFlexHost _flexHost;

        public CompanyUser()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<CompanyUser>>();
        }

        public CompanyUser(ILogger<CompanyUser> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        #region "Attributes"



        #region "Protected"
        public bool IsFrontEndStaff { get; protected set; }

        [StringLength(50)]
        public string? DomainId { get; set; }

        [StringLength(50)]
        public string? EmployeeId { get; set; }

        [StringLength(50)]
        public string? PrimaryContactAreaCode { get; protected set; }

        public CompanyUser SinglePointReportingManager { get; protected set; }

        [StringLength(32)]
        public string? SinglePointReportingManagerId { get; protected set; }

        public BaseBranch BaseBranch { get; protected set; }

        [StringLength(32)]
        public string? BaseBranchId { get; protected set; }

        public Company Company { get; protected set; }

        [StringLength(32)]
        public string? CompanyId { get; protected set; }

        public int? TrailCap { get; set; }
        //public List<CompanyUserARMScopeOfWork> ARMScopeOfWork { get; set; }
        //public List<CompanyUserScopeOfWork> ScopeOfWork { get; set; }
        public List<CompanyUserDesignation> Designation { get; set; }
        public CompanyUserWorkflowState CompanyUserWorkflowState { get; set; }
        public List<CompanyUserPlaceOfWork> PlaceOfWork { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Public Methods"

        public void SetUserId(string userId)
        {
            UserId = userId;
        }

        #endregion "Public Methods"
    }
}