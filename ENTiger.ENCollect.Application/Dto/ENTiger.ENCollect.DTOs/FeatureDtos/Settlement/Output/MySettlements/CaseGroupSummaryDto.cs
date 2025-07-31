using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public class CaseGroupSummaryDto : DtoBridge
    {
        public string Status { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
    
}
