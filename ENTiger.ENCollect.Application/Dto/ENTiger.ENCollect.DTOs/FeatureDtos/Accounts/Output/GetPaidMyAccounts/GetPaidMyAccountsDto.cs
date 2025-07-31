namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPaidMyAccountsDto : DtoBridge
    {
        public string? Id { get; set; }
        public DateTime? PTPDate { get; set; }
        public string? Bucket { get; set; }
        public decimal? POS { get; set; }
        public string? AccountNo { get; set; }
        public string? CustomerName { get; set; }
        public string? ProductCode { get; set; }
        public string? Area { get; set; }

        private bool paid = true;
        public bool IsPaid
        { get { return paid; } set { paid = value; } }
        public string? carediCardNo { get; set; }
        public int ExpiresInDays { get; set; }
        public DateTime? CollectorAllocationExpiryDate { get; set; }
        public string AllocationExpiryColor { get; set; } = "green";
        public string? CustomerID { get; set; }
    }
}