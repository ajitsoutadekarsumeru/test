using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [StringLength(200)]
        public string? PrimaryEMail { get; set; }

        [StringLength(50)]
        public string? PrimaryMobileNumber { get; set; }

        [StringLength(50)]
        public string? SecondaryContactNumber { get; set; }

        public string? Remarks { get; set; }
        public string? Role { get; set; }
        public bool IsBlackListed { get; set; }
        public bool? IsDeactivated { get; set; }
        public string? DeactivationReason { get; set; }
        public string? IsFrontEndStaff { get; set; }
        public string? IsSaveAsDraft { get; set; }
        public string? SinglePointReportingManagerId { get; set; }
        public string? BaseBranchId { get; set; }
        public string? CompanyId { get; set; }
        public string? EmployeeId { get; set; }
        public string? DesignationId { get; set; }
        public string? DepartmentId { get; set; }
        public string? DomainId { get; set; }
        public string? PrimaryContactAreaCode { get; set; }
        public string? PrimaryContactCountryCode { get; set; }
        public string? Status { get; set; }
        public string? yCoreBankingId { get; set; }
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }
        public string? WorkOfficeLongitude { get; set; }
        public string? WorkOfficeLattitude { get; set; }
        public int? load { get; set; }
        public decimal? experiance { get; set; }
        public decimal WalletLimit { get; set; }
        public string UserType { get; set; }
        public ICollection<UserDesignationDto> Roles { get; set; }        
        public ICollection<ChangeLogInfoDto> ChangeLogInfo { get; set; }
        public ICollection<LanguageDto> Languages { get; set; }
        public ICollection<CompanyUserPlaceOfWorkDto> companyUserPlaceOfWorks { get; set; }
        public CreditAccountDetailsDto CreditAccountDetails { get; set; }
        //public ICollection<UserCustomerPersonaDto> usercustomerpersona { get; set; }
        //public ICollection<UserPerformanceBandDto> userperformanceband { get; set; }

        public string? ProductLevelId { get; set; }
        public string? GeoLevelId { get; set; }
        public ICollection<UserProductScopeDto>? ProductScopes { get; set; }
        public ICollection<UserGeoScopeDto>? GeoScopes { get; set; }
        public ICollection<UserBucketScopeDto>? BucketScopes { get; set; }
    }
}