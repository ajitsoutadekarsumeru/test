namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCollectionsDto : DtoBridge
    {
        public string? ReceiptNo { get; set; }

        public string? CustomerName { get; set; }

        public string? AccountNo { get; set; }

        public string? PaymentMode { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? InstrumentNo { get; set; }

        public DateTime? InstrumentDate { get; set; }

        public string? BankName { get; set; }

        public string? BranchName { get; set; }

        public string? ProductName { get; set; }

        public string? Bankcity { get; set; }

        public string? IfscCode { get; set; }

        public string? yForeClosureAmount { get; set; }

        public string? yOverdueAmount { get; set; }

        public string? yBounceCharges { get; set; }

        public string? yPenalInterest { get; set; }

        public string? ChequeInstrumentNo { get; set; }

        public string? Id { get; set; }

        public string? MICR { get; set; }

        public string? DepositBranchName { get; set; }

        public string? Settlement { get; set; }

        public string? OtherCharges { get; set; }

        public string? TransactionNumber { get; set; }

        public string? EBCCharge { get; set; }

        public string? CollectionPickupCharge { get; set; }

        // public string? Receipt { get; set; }
        public string? Account { get; set; }

        public string? Cheque { get; set; }
        public string? ProductGroup { get; set; }
    }
}