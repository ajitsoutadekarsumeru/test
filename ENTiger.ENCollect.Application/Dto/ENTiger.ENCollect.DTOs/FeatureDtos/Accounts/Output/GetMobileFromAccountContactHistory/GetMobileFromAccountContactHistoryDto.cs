namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMobileFromAccountContactHistoryDto : DtoBridge
    {
        public string MobileNo { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ContactSource { get; set; } 
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
