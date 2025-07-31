using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class FeedbackProjection : PersistenceModel
    {
        protected readonly ILogger<FeedbackProjection> _logger;

        public FeedbackProjection()
        {
        }

        public FeedbackProjection(ILogger<FeedbackProjection> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string FeedbackId { get; set; }
        public Feedback Feedback { get; set; }

        public long? BUCKET { get; set; }

        [StringLength(50)]
        public string? CURRENT_BUCKET { get; set; }

        [StringLength(50)]
        public string? NPA_STAGEID { get; set; }


        [StringLength(32)]
        public string? AgencyId { get; set; }

        public ApplicationOrg Agency { get; set; }

        [StringLength(32)]
        public string? CollectorId { get; set; }

        public ApplicationUser Collector { get; set; }

        [StringLength(32)]
        public string? TeleCallingAgencyId { get; set; }

        public ApplicationOrg TeleCallingAgency { get; set; }

        [StringLength(32)]
        public string? TeleCallerId { get; set; }

        public ApplicationUser TeleCaller { get; set; }

        [StringLength(32)]
        public string? AllocationOwnerId { get; set; }

        public ApplicationUser AllocationOwner { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? BOM_POS { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CURRENT_POS { get; set; }

        [StringLength(50)]
        public string? PAYMENTSTATUS { get; set; }


        public DateTime? LastDispositionDate { get; set; }

        [StringLength (50)]
        public string? LastDispositionCode { get; set; }

        [StringLength(50)]
        public string? LastDispositionCodeGroup { get; set; }

        public DateTime? LastPTPDate { get; set; }


        #endregion "Public"

        #endregion "Attributes"
    }
}