namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionsForCancellationDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? ReceiptNo { get; set; }

        public string? ReceiptId { get; set; }

        public string? ReceiptDate { get; set; }

        public string? CollectorName { get; set; }

        public string? CollectorId { get; set; }

        public string? CollectorCustomId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerAccountNo { get; set; }

        public string? MobileNo { get; set; }

        public string? EmailId { get; set; }

        public decimal? OverdueAmount { get; set; }

        public decimal? BounceCharges { get; set; }

        public decimal? PenalAmount { get; set; }

        public decimal? ForeclosureAmount { get; set; }

        public decimal? Total { get; set; }

        public string? AgencyCode { get; set; }

        public string? AgencyName { get; set; }

        public decimal? EmiAmount { get; set; }

        public string? ProductName { get; set; }

        public string? TransactionNumber { get; set; }

        public string? AgreementId { get; set; }

        public string? EBCCharge { get; set; }

        public string? CollectionPickupCharge { get; set; }

        public string? CancellationRemarks { get; set; }
    }
}