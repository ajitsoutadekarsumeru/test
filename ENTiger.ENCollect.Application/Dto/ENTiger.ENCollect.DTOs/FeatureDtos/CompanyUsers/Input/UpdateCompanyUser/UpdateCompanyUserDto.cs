using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class UpdateCompanyUserDto : DtoBridge
    {
        public string Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string PrimaryEMail { get; set; }

        public string? PrimaryMobileNumber { get; set; }

        [RegularExpression("^[a-zA-Z0-9''\\-'.''_'','\\s]{1,200}$", ErrorMessage = "Invalid Remarks.")]
        public string? Remarks { get; set; }

        public string? Role { get; set; }
        public bool IsBlackListed { get; set; }
        public string? BaseBranchId { get; set; }
        public string? SinglePointReportingManagerId { get; set; }
        public bool IsFrontEndStaff { get; set; }
        public string? BlackListingReason { get; set; }
        public string? CoreBankingID { get; set; }
        public string? EmployeeID { get; set; }
        public string? IsSaveAsDraft { get; set; }
        public string? DomainId { get; set; }
        public string? PrimaryContactAreaCode { get; set; }
        public string? PrimaryContactCountryCode { get; set; }
        public string? userid { get; set; }
        public bool IsUserEdited { get; set; }
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public string? WorkOfficeLongitude { get; set; }
        public string? WorkOfficeLattitude { get; set; }
        public int? load { get; set; }
        public decimal? Experience { get; set; }
        public decimal WalletLimit { get; set; }
        public string UserType { get; set; }
        public ICollection<UserDesignationDto>? Roles { get; set; }
        //public ICollection<CompanyUserARMScopeOfWorkDto>? ARMScopeOfWork { get; set; }
        //public ICollection<CompanyUserScopeOfWorkDto>? ScopeOfWork { get; set; }
        public CreditAccountDetailsDto? CreditAccountDetails { get; set; }
        //public ICollection<UserCustomerPersonaDto>? usercustomerpersona { get; set; }
        //public ICollection<UserPerformanceBandDto>? userperformanceband { get; set; }
        public AddressDto? address { get; set; }
        public List<CompanyUserPlaceOfWorkDto>? PlaceOfWork { get; set; }

        public string? ProductLevelId { get; set; }
        public string? GeoLevelId { get; set; }
        public List<UserProductScopeDto>? ProductScopes { get; set; }
        public List<UserGeoScopeDto>? GeoScopes { get; set; }
        public List<UserBucketScopeDto>? BucketScopes { get; set; }
    }
}