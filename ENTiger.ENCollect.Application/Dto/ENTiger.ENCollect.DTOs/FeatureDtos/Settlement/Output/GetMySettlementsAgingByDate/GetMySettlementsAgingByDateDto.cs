
namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMySettlementsAgingByDateDto : DtoBridge
    {
        public string RangeOfAging { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
