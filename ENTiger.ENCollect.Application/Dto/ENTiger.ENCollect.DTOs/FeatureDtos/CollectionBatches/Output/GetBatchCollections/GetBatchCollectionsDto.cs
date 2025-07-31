namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBatchCollectionsDto : DtoBridge
    {
        public string? AccountNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? ReceiptNumber { get; set; }
        public decimal? ReceiptAmount { get; set; }
        public DateTimeOffset ReceiptDate { get; set; }
    }
}