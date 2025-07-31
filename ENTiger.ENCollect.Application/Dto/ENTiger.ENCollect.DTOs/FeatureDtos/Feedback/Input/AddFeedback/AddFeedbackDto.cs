using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class AddFeedbackDto : DtoBridge
    {
        [Required]
        public string? Accountno { get; set; }
        [Required]
        public string? CustomerMet { get; set; }
        [Required]
        public string? DispositionCode { get; set; }
        public DateTime? PTPDate { get; set; }
        public decimal? PTPAmount { get; set; }
        public string? DeliquencyReason { get; set; }
        public string? EscalateTo { get; set; }
        public string? Remarks { get; set; }
        public string? IsReallocationRequest { get; set; }
        public string? ReallocationRequestReason { get; set; }
        public string? NewArea { get; set; }
        public string? NewAddress { get; set; }
        public string? City { get; set; }
        public string? NewContactNo { get; set; }
        public DateTime? DispositionDate { get; set; }
        public string? RightPartyContact { get; set; }
        public string? NextAction { get; set; }
        public string? NonPaymentReason { get; set; }
        public string? AssignTo { get; set; }
        public string? AssignReason { get; set; }
        public string? NewContactCountryCode { get; set; }
        public string? NewContactAreaCode { get; set; }
        public string? State { get; set; }
        public string? NewEmailId { get; set; }
        public string? PickAddress { get; set; }
        public string? OtherPickAddress { get; set; }
        public string? LeadId { get; set; }
        public string? LeadBaseId { get; set; }
        [Required]
        public string? DispositionGroup { get; set; }
        public string? AssigneeId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? OfflineFeedbackDate { get; set; }
        public string? UploadedFileName { get; set; }
        public string? GeoLocation { get; set; }
        public string? Place_of_visit { get; set; }
        public string? ThirdPartyContactPerson { get; set; }
        public string? ModeOfCommunication { get; set; }

    }
}