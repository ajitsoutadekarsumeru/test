using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Agency : ApplicationOrg
    {
        protected readonly ILogger<Agency> _logger;
        protected readonly IFlexHost _flexHost;

        public Agency()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<Agency>>();
        }

        public Agency(ILogger<Agency> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(500)]
        public string? Remarks { get; set; }

        public bool IsBlackListed { get; set; }

        [StringLength(200)]
        public string? BlackListingReason { get; set; }

        public DateTime? ContractExpireDate { get; set; }
        public DateTime? LastRenewalDate { get; set; }
        public DateTime? FirstAgreementDate { get; set; }

        [StringLength(50)]
        public string? ServiceTaxRegistrationNumber { get; set; }

        [StringLength(200)]
        public string? DeactivationReason { get; set; }

        public bool IsParentAgency { get; set; }

        [StringLength(50)]
        public string? PAN { get; set; }

        [StringLength(50)]
        public string? TIN { get; set; }

        [StringLength(50)]
        public string? GSTIN { get; set; }

        [StringLength(50)]
        public string? PrimaryOwnerFirstName { get; set; }
        [StringLength(50)]
        public string? PrimaryOwnerMiddleName { get; set; }

        [StringLength(50)]
        public string? PrimaryOwnerLastName { get; set; }

        [StringLength(50)]
        public string? PrimaryContactCountryCode { get; set; }

        [StringLength(50)]
        public string? PrimaryContactAreaCode { get; set; }

        [StringLength(50)]
        public string? SecondaryContactCountryCode { get; set; }

        [StringLength(50)]
        public string? SecondaryContactAreaCode { get; set; }

        [StringLength(100)]
        public string? YardAddress { get; set; }

        public decimal? NumberOfYards { get; set; }
        public decimal? YardSize { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        public Department Department { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        public Designation Designation { get; set; }

        [StringLength(32)]
        public string? AgencyTypeId { get; set; }

        public AgencyType AgencyType { get; set; }

        [StringLength(32)]
        public string? ParentAgencyId { get; set; }

        public Agency ParentAgency { get; set; }

        [StringLength(32)]
        public string? RecommendingOfficerId { get; set; }

        public CompanyUser RecommendingOfficer { get; set; }

        [StringLength(32)]
        public string? CreditAccountDetailsId { get; set; }

        public CreditAccountDetails CreditAccountDetails { get; set; }

        [StringLength(32)]
        public string? AgencyCategoryId { get; set; }

        public AgencyCategory AgencyCategory { get; set; }

        [StringLength(32)]
        public string? AddressId { get; set; }

        public Address Address { get; set; }

        public AgencyWorkflowState AgencyWorkflowState { get; set; }
        public List<AgencyScopeOfWork> ScopeOfWork { get; set; }
        public List<AgencyPlaceOfWork> PlaceOfWork { get; set; }
        public List<AgencyIdentification> AgencyIdentifications { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}