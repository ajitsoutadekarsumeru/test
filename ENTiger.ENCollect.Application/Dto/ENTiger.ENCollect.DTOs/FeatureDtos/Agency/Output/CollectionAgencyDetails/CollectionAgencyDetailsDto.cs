using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CollectionAgencyDetailsDto : DtoBridge
    {
        #region Attributes

        #region PartyAttributes

        [StringLength(256)]
        public string Id { get; set; }

        public string ActivationCode { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        public bool isOrganization { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(200)]
        public string PrimaryEMail { get; set; }

        [StringLength(50)]
        public string PrimaryMobileNumber { get; set; }

        [StringLength(500)]
        public string ProfileImage { get; set; }

        [StringLength(50)]
        public string SecondaryContactNumber { get; set; }

        #endregion PartyAttributes

        public string Status { get; set; }
        public string AgencyCode { get; set; }
        public string Remarks { get; set; }
        public bool? IsBlackListed { get; set; }
        public string BlackListingReason { get; set; }
        public DateTime? ContractExpireDate { get; set; }
        public DateTime? LastRenewalDate { get; set; }
        public DateTime? FirstAgreementDate { get; set; }
        public string ServiceTaxRegistrationNumber { get; set; }

        [StringLength(32)]
        public string CollectionAgencyTypeId { get; set; }

        public string CollectionAgencyType { get; set; }
        public string CollectionAgencySubType { get; set; }

        [StringLength(32)]
        public string RecommendingOfficerId { get; set; }

        public AddressDto Address { get; set; }
        public CreditAccountDetailsDto CreditAccountDetails { get; set; }
        public ICollection<AgencyScopeOfWorkDto> ScopeOfWork { get; set; }
        public ICollection<AgencyPlaceOfWorkDto> PlaceOfWork { get; set; }
        public ICollection<AgencyIdentificationDto> ProfileIdentification { get; set; }

        //public ICollection<ChangeLogInfoDto> ChangeLogInfo { get; set; }
        public string PAN { get; set; }

        public string TIN { get; set; }
        public string GSTIN { get; set; }
        public string PrimaryOwnerLastName { get; set; }
        public string PrimaryOwnerFirstName { get; set; }
        public string? PrimaryOwnerMiddleName { get; set; }
        public decimal? NumberOfYards { get; set; }
        public decimal? YardSize { get; set; }
        public string YardAddress { get; set; }
        public string PrimaryContactCountryCode { get; set; }
        public string SecondaryContactAreaCode { get; set; }
        public string SecondaryContactCountryCode { get; set; }
        public string PrimaryContactAreaCode { get; set; }
        public bool IsParentAgency { get; set; }
        public string ParentAgencyId { get; set; }
        public string DiallerName { get; set; }
        public string AgencyCommissionNWOF { get; set; }
        public string AgencyCommissionWOF { get; set; }
        public string GCTPercentage { get; set; }
        public ICollection<AgencyChangeLogInfoDto>? ChangeLogInfo { get; set; }

        #endregion Attributes
    }
}