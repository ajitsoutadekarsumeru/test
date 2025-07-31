namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPrimaryAllocationDownloadDto : DtoBridge
    {
        public string? InputJson { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? Status { get; set; }
        public string? CustomId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}