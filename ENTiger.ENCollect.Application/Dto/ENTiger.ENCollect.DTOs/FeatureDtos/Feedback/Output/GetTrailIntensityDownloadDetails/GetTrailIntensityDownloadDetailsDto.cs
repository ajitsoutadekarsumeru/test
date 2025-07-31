namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTrailIntensityDownloadDetailsDto : DtoBridge
    {
        public string? TransactionId { get; set; }
        public string? FileName { get; set; }
        public string? Status { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string? Id { get; set; }
    }
}