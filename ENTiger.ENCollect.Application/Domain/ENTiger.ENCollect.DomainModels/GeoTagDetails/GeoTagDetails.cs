using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class GeoTagDetails : DomainModelBridge
    {
        protected readonly ILogger<GeoTagDetails> _logger;

        public GeoTagDetails()
        {
        }

        public GeoTagDetails(ILogger<GeoTagDetails> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(200)]
        public string? GeoTagReason { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [StringLength(500)]
        public string? GeoLocation { get; set; }

        [StringLength(32)]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        [StringLength(50)]
        public string? TransactionType { get; set; }
        [StringLength(32)]
        public string? AccountId { get; set; }

        public LoanAccount Account { get; set; }
        [StringLength(20)]
        public string? TransactionSource { get; set; }

        #endregion "Public"

        #endregion "Attributes"

        #region "Private Methods"

        public void SetCreatedBy(string loggedInUserId)
        {
            this.CreatedBy = loggedInUserId;
        }

        public void SetAccount(string accountId)
        {
            this.AccountId = accountId;
        }
        #endregion "Private Methods"
    }
}