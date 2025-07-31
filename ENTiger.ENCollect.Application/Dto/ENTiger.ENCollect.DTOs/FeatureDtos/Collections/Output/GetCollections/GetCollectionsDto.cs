namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? ReceiptNo { get; set; }
        public string? ReceiptDate { get; set; }
        public string? CollectorName { get; set; }
        public string? CollectorId { get; set; }
        public string? CollectorCustomId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAccountNo { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public decimal? Total { get; set; }
        public string? yForeClosureAmount { get; set; }
        public string? yOverdueAmount { get; set; }
        public string? yBounceCharges { get; set; }
        public string? yPenalInterest { get; set; }
        public string? Product { get; set; }
        public string? CollectionMode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}