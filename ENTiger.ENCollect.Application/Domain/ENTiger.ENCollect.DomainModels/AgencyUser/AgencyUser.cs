using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUser : ApplicationUser
    {
        protected readonly ILogger<AgencyUser> _logger;
        protected readonly IFlexHost _flexHost;

        public AgencyUser()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<AgencyUser>>();
        }

        public AgencyUser(ILogger<AgencyUser> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? IdCardNumber { get; protected set; }

        public DateTime? EmploymentDate { get; protected set; }
        public DateTime? AuthorizationCardExpiryDate { get; protected set; }
        public DateTime? LastRenewalDate { get; protected set; }

        [StringLength(50)]
        public string? SupervisorEmailId { get; protected set; }

        [StringLength(50)]
        public string? PrimaryContactAreaCode { get; protected set; }

        [StringLength(50)]
        public string? DiallerId { get; protected set; }

        [StringLength(50)]
        public string? IDType { get; protected set; }

        [StringLength(50)]
        public string? UDIDNumber { get; protected set; }

        [StringLength(50)]
        public string? FatherName { get; protected set; }

        [StringLength(200)]
        public string? DisableReason { get; protected set; }

        [StringLength(200)]
        public string? DeactivateReason { get; protected set; }

        public bool IsPrinted { get; protected set; }

        [StringLength(50)]
        public string? yCoreBankingId { get; set; }

        public DateTime? DRACertificateDate { get; set; }
        public DateTime? DRATrainingDate { get; set; }

        [StringLength(50)]
        public string? DRAUniqueRegistrationNumber { get; set; }

        public int? Age { get; set; }

        [StringLength(20)]
        public string DRAStatus { get; set; }

        [StringLength(32)]
        public string? AddressId { get; protected set; }

        public Address? Address { get; protected set; }

        [StringLength(32)]
        public string? AgencyId { get; set; }

        public Agency? Agency { get; protected set; }

        public AgencyUserWorkflowState AgencyUserWorkflowState { get; set; }
        public List<AgencyUserDesignation>? Designation { get; set; }        
        public List<AgencyUserPlaceOfWork>? PlaceOfWork { get; set; }
        public List<AgencyUserIdentification>? AgencyUserIdentifications { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Public Methods"

        public void SetUserId(string userId)
        {
            UserId = userId;
        }
        public void SetIdCardNumber(string idCardNumber)
        {
            IdCardNumber = idCardNumber;
        }

        #endregion "Public Methods"
    }
}