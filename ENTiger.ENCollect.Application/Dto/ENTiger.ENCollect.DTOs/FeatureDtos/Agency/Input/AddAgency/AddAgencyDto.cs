using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AddAgencyDto : DtoBridge
    {
        #region Attributes

        #region PartyAttributes

        [StringLength(256)]
        public string? ActivationCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        [Required]
        public string FirstName { get; set; }

        public bool isOrganization { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(200)]
        public string? PrimaryEMail { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid Mobile Number.")]
        public string? PrimaryMobileNumber { get; set; }

        [StringLength(500)]
        public string? ProfileImage { get; set; }

        [StringLength(50)]
        public string? SecondaryContactNumber { get; set; }

        #endregion PartyAttributes
        [Required]
        public string? AgencyCode { get; set; }

        [RegularExpression("^[a-zA-Z0-9''\\-'.''_'','\\s]{1,200}$", ErrorMessage = "Invalid Remarks.")]
        public string? Remarks { get; set; }

        public string? DepartmentID { get; set; }
        public string? DesignationID { get; set; }
        public bool? IsBlackListed { get; set; }
        public string? BlackListingReason { get; set; }

        [Required]
        public DateTime? ContractExpireDate { get; set; }

        [Required]
        public DateTime? LastRenewalDate { get; set; }

        [Required]
        public DateTime? FirstAgreementDate { get; set; }

        public string? ServiceTaxRegistrationNumber { get; set; }
        public bool isSaveAsDraft { get; set; }

        [StringLength(32)]
        public string? CollectionAgencyTypeId { get; set; }

        [Required]
        [StringLength(32)]
        public string? RecommendingOfficerId { get; set; }

        [Required]
        public bool IsParentAgency { get; set; }

        public string? ParentAgencyId { get; set; }

        [Required]
        public string PAN { get; set; }

        [Required]
        public string TIN { get; set; }

        public string? GSTIN { get; set; }

        [Required]
        public string PrimaryOwnerFirstName { get; set; }

        [Required]
        public string PrimaryOwnerLastName { get; set; }
        public string? PrimaryOwnerMiddleName { get; set; }

        public string? PrimaryContactCountryCode { get; set; }
        public string? PrimaryContactAreaCode { get; set; }
        public string? SecondaryContactCountryCode { get; set; }
        public string? SecondaryContactAreaCode { get; set; }
        public AddressDto Address { get; set; }
        public CreditAccountDetailsDto? CreditAccountDetails { get; set; }
        public DepartmentDto? Department { get; set; }
        public DesignationDto? Designation { get; set; }
        public AgencyTypeDto? AgencyType { get; set; }
        public decimal? NumberOfYards { get; set; }
        public decimal? YardSize { get; set; }
        public string? YardAddress { get; set; }
        public string? AgencyCategoryId { get; set; }
        public string? AgencyCommissionNWOF { get; set; }
        public string? AgencyCommissionWOF { get; set; }
        public string? GCTPercentage { get; set; }
        public ICollection<AgencyScopeOfWorkDto>? ScopeOfWork { get; set; }
        public ICollection<AgencyPlaceOfWorkDto>? PlaceOfWork { get; set; }
        public ICollection<AgencyIdentificationDto>? ProfileIdentification { get; set; }

        #endregion Attributes
    }
}