using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AddAgentDto : DtoBridge
    {
        [Required]
        public string? AgencyId { get; set; }

        public bool isSaveAsDraft { get; set; }

        [Required]
        public DateTime? AuthorizationCardExpiryDate { get; set; }

        [Required]
        public DateTime? LastRenewalDate { get; set; }

        [Required]
        public DateTime? EmploymentDate { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? PrimaryMobileNumber { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string? PrimaryEMail { get; set; }

        public string? SecondaryContactNumber { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string? ProfileImage { get; set; }

        [RegularExpression("^[a-zA-Z0-9''\\-'.''_'','\\s]{1,200}$", ErrorMessage = "Invalid Remarks.")]
        public string? Remarks { get; set; }

        public bool IsBlackListed { get; set; }
        public string? BlackListingReason { get; set; }
        public string? ServiceTaxRegistrationNumber { get; set; }
        public string? DiallerId { get; set; }
        public string? IDType { get; set; }
        public string? UDIDNumber { get; set; }
        public string? SupervisorEmailId { get; set; }
        public decimal? Experience { get; set; }
        public string? PrimaryContactCountryCode { get; set; }
        public string PrimaryContactAreaCode { get; set; }
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }
        public string? WorkOfficeLongitude { get; set; }
        public string? WorkOfficeLattitude { get; set; }
        public int? load { get; set; }
        public DateTime? DRACertificateDate { get; set; }
        public DateTime? DRATrainingDate { get; set; }
        public string? DRAUniqueRegistrationNumber { get; set; }
        public string? yCoreBankingId { get; set; }
        public string? overwriteAgentId { get; set; }
        public string? DRAStatus { get; set; }
        public int? Age { get; set; }
        [Required]
        [StringLength(5)]
        public string? BloodGroup { get; set; }
        [Required]
        [StringLength(10)]
        public string? EmergencyContactNo { get; set; }
        public decimal WalletLimit { get; set; }
        public string UserType { get; set; }
        public List<UserDesignationDto> Roles { get; set; }
        public ICollection<AgencyUserIdentificationDto> profileIdentification { get; set; }
        public List<AgencyUserPlaceOfWorkDto> PlaceOfWork { get; set; }
        public AddressDto Address { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public CreditAccountDetailsDto CreditAccountDetails { get; set; }

        public string? ProductLevelId { get; set; }
        public string? GeoLevelId { get; set; }
        public List<UserProductScopeDto>? ProductScopes { get; set; }
        public List<UserGeoScopeDto>? GeoScopes { get; set; }
        public List<UserBucketScopeDto>? BucketScopes { get; set; }
    }
}