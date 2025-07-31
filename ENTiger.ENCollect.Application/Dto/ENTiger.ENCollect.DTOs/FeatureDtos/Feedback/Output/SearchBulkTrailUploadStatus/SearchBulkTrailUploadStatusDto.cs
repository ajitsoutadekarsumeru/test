namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchBulkTrailUploadStatusDto : DtoBridge
    {
        public string? TransactionId { get; set; }
        public string? FileName { get; set; }
        public string? Status { get; set; }
        public string? DownloadFileName { get; set; }
    }
}