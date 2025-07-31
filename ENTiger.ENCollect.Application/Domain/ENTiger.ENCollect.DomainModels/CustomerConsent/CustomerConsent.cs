using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CustomerConsent : DomainModelBridge
    {
        protected readonly ILogger<CustomerConsent> _logger;

        protected CustomerConsent()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<CustomerConsent>>();
        }

        #region "Attributes"

        #region "Public"
        [StringLength(32)]
        public string? AccountId { get; set; }
        public LoanAccount Account { get; set; }
        [StringLength(32)]
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }

        
        public DateTime? RequestedVisitTime { get; set; }  //7pm - 7am
        public DateTime? ConsentResponseTime { get; set; }  // Nullable, only filled when user responds
        public DateTime? ExpiryTime { get; set; }   //cronjob to run @ 8am  every morning
        public bool? IsActive { get; set; }  = true;
        public string Status { get; set; } = CustomerConsentStatusEnum.Pending.Value;  // Enum for Pending, Accepted, Rejected, Expired
        public string SecureToken { get; set; }  //  Unique identifier for the response
        #endregion

        #region "Protected"
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion

    }
}
