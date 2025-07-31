namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryUnAllocationBatchStatusDto : DtoBridge
    {
        public string? TransactionId { get; set; }
        public string? FileName { get; set; }
        public string? Status { get; set; }
        public string? DownloadFileName { get; set; }
        public string? UnAllocationType { get; set; }
    }
}