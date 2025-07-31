using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMySettlementDetailsByAgingDto : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
        public string AccountId { get; set; }
        public string TOS { get; set; }
        public string Status { get; set; }
        public DateTimeOffset StatusChangedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public decimal SettlementAmount { get; set; }
        public string RejectionReason { get; set; }
        public int AgingInCurrentStatusDays { get; set; }
        public decimal? RenegotiationAmount { get; set; }
    }
}
