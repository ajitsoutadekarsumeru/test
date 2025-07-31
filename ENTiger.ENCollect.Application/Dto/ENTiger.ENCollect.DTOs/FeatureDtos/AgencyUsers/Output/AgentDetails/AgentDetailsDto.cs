using ENTiger.ENCollect.CommonModule;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgentDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? Remarks { get; set; }
        public string? DisableReason { get; set; }
        public string? DeactivateReason { get; set; }
        public string? RejectionReason { get; set; }
        public bool? IsBlackListed { get; set; }
        public string? BlackListingReason { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public DateTime? AuthorizationCardExpiryDate { get; set; }
        public string? PrimaryEMail { get; set; }
        public string? PrimaryMobileNumber { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AgencyCode { get; set; }
        public string? Status { get; set; }
        public DateTime? LastRenewalDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? AgencyFirstName { get; set; }
        public string? FatherName { get; set; }
        public string? AgencyLastName { get; set; }
        public bool IsPrintValid { get; set; }
        public string? SupervisorEmailId { get; set; }
        public string? DiallerId { get; set; }
        public string? IDType { get; set; }
        public string? UDIDNumber { get; set; }
        public string? Experience { get; set; }
        public string? AgencyId { get; set; }
        public string? PrimaryContactAreaCode { get; set; }
        public bool? IsPrinted { get; set; }
        public string? IdCardNumber { get; set; }
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }
        public string? WorkOfficeLongitude { get; set; }
        public string? WorkOfficeLattitude { get; set; }
        public string? PrimaryContactCountryCode { get; set; }
        public int? load { get; set; }
        public decimal? experiance { get; set; }
        public DateTime? DRACertificateDate { get; set; }
        public DateTime? DRATrainingDate { get; set; }
        public string? DRAUniqueRegistrationNumber { get; set; }
        public string? yCoreBankingId { get; set; }
        public int? Age { get; set; }
        public string? DRAStatus { get; set; }
        public string? PhysicalIDcardNumber { get; set; }
        public string? BloodGroup { get; set; }
        public string? EmergencyContactNo { get; set; }
        public decimal WalletLimit { get; set; }
        public string UserType { get; set; }
        public AddressDto? Address { get; set; }
        public AgentBankDetailsDto? CreditAccountDetails { get; set; }
        public ICollection<LanguageDto>? Languages { get; set; }
        public ICollection<ProfileIdentificationDto>? ProfileIdentification { get; set; }        
        public ICollection<AgencyUserPlaceOfWorkDto>? PlaceOfWork { get; set; }
        public ICollection<UserDesignationOutputApiModel>? Roles { get; set; }
        public ICollection<AgentChangeLogInfoDto>? ChangeLogInfo { get; set; }

        public string? ProductLevelId { get; set; }
        public string? GeoLevelId { get; set; }
        public ICollection<UserProductScopeDto>? ProductScopes { get; set; }
        public ICollection<UserGeoScopeDto>? GeoScopes { get; set; }
        public ICollection<UserBucketScopeDto>? BucketScopes { get; set; }
    }
}