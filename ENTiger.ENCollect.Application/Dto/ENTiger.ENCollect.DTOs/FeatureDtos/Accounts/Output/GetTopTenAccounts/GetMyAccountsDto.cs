namespace ENTiger.ENCollect.DTOs.FeatureDtos.Accounts.Output.GetTopTenAccounts
{
    public partial class GetMyTopTenAccountsDto : DtoBridge
    {
        public string Id { get; set; }
        public DateTime? PTPDate { get; set; }

        //public long? Bucket { get; set; }
        public string Bucket { get; set; }

        public decimal? POS { get; set; }
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string Area { get; set; }
        public bool IsPaid { get; set; }
        public string carediCardNo { get; set; }
        public string MinimumAmountDue { get; set; }
        public string TotalBalanceOutStanding { get; set; }
        public string CarediCardNo { get; set; }
        public string SubProductCode { get; set; }
        public string PRODUCT { get; set; }
        public string SubProduct { get; set; }
        public string ProductGroup { get; set; }
    }
}