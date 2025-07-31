using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public class MySettlementSummaryDetailsDto : DtoBridge
    {
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public string ProductGroup { get; set; }
        public string CustomerName { get; set; }
        public string TOS { get; set; }
        public string Status { get; set; }
        public decimal SettlementAmount { get; set; }
        public decimal? RenegotiationAmount { get; set; }
        public string? RejectionReason { get; set; }
        public string CustomId { get; set; }
        public string AccountId { get; set; }
        public DateTimeOffset StatusUpdatedOn { get; set; }
        public DateTimeOffset CreatedDate  { get; set; }
        public int AgingInCurrentStatusDays { get; set; }
    }
}
