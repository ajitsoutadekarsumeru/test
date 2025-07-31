namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchUpdateUsersByBatchStatusDto : DtoBridge
    {
        public string TransactionId { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string DownloadFileName { get; set; }
    }
}