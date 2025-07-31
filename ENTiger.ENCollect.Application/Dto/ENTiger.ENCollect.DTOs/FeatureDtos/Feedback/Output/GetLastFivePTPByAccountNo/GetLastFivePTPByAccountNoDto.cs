namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetLastFivePTPByAccountNoDto : DtoBridge
    {
        public DateTime FeedbackDate { get; set; }
        public string? DispositionCode { get; set; }
        public DateTime? PTPDate { get; set; }
        public decimal? PTPAmount { get; set; }
        public string? CollectorFirstName { get; set; }
        public string? CollectorLastName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Remarks { get; set; }
        public string? UploadedFileName { get; set; }
        public Boolean? groupLoanAttendance { get; set; }
        public string? RightPartyContact { get; set; }
        public string? CustomerMet { get; set; }
        public string? NewContactNumber { get; set; }
        public string? NewArea { get; set; }
        public string? NewAddress { get; set; }
    }
}