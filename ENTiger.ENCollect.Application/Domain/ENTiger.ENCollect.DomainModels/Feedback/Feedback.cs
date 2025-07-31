using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Feedback : DomainModelBridge
    {
        protected readonly ILogger<Feedback> _logger;

        public Feedback()
        {
        }

        public Feedback(ILogger<Feedback> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(200)]
        public string? UploadedFileName { get; set; }

        [StringLength(100)]
        public string? CustomerMet { get; set; }

        [StringLength(100)]
        public string? DispositionCode { get; set; }

        [StringLength(200)]
        public string? DispositionGroup { get; set; }

        public DateTime? PTPDate { get; set; }

        [StringLength(200)]
        public string? EscalateTo { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        public DateTime? FeedbackDate { get; set; }

        [StringLength(20)]
        public string? IsReallocationRequest { get; set; }

        [StringLength(500)]
        public string? ReallocationRequestReason { get; set; }

        [StringLength(200)]
        public string? NewArea { get; set; }

        [StringLength(500)]
        public string? NewAddress { get; set; }

        [StringLength(200)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? NewContactNo { get; set; }

        public DateTime? DispositionDate { get; set; }

        [StringLength(50)]
        public string? RightPartyContact { get; set; }

        [StringLength(50)]
        public string? NextAction { get; set; }

        [StringLength(100)]
        public string? NonPaymentReason { get; set; }

        //[StringLength(50)]
        //public string AssignTo { get; set; }
        [StringLength(100)]
        public string? AssignReason { get; set; }

        [StringLength(50)]
        public string? NewContactCountryCode { get; set; }

        [StringLength(50)]
        public string? NewContactAreaCode { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? NewEmailId { get; set; }

        [StringLength(100)]
        public string? PickAddress { get; set; }

        [StringLength(100)]
        public string? OtherPickAddress { get; set; }

        [StringLength(50)]
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
        public DateTime? OfflineFeedbackDate { get; set; }

        public ApplicationUser Collector { get; set; }

        [StringLength(32)]
        public string? CollectorId { get; set; }

        [ForeignKey("AssigneeId")]
        public ApplicationUser Assignee { get; set; }

        [StringLength(32)]
        public string? AssigneeId { get; set; }

        public ApplicationUser User { get; set; }

        [StringLength(32)]
        public string? UserId { get; set; }

        [StringLength(500)]
        public string? GeoLocation { get; set; }

        public LoanAccount Account { get; set; }

        [StringLength(32)]
        public string AccountId { get; set; }

        [StringLength(50)]
        public string? AssignTo { get; set; }

        public decimal? PTPAmount { get; set; }

        [StringLength(50)]
        public string? DeliquencyReason { get; set; }

        [StringLength(50)]
        public string? ThirdPartyContactPerson { get; set; }

        [StringLength(50)]
        public string? ThirdPartyContactNo { get; set; }

        [StringLength(500)]
        public string? Place_of_visit { get; set; }
        [StringLength(100)]
        public string? ModeOfCommunication { get; set; }

        [StringLength(20)]
        public string? TransactionSource { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}