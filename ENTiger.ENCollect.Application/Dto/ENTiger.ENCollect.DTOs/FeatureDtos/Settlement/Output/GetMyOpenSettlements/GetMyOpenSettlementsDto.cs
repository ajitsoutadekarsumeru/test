namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyOpenSettlementsDto : DtoBridge
    {
        public string Status { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
