namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchMastersImportStatusDto : DtoBridge
    {
        public string FileType { get; set; }
        public string? TransactionId { get; set; }

        public string? FileName { get; set; }

        public string? Status { get; set; }

        public DateTime UploadedDate { get; set; }

        public DateTime ProcessedDate { get; set; }
    }
}