using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public class MySettlementsSummaryDto : DtoBridge
    {
        public List<CaseGroupSummaryDto> OpenCases { get; set; } = new List<CaseGroupSummaryDto>();
        public List<CaseGroupSummaryDto> ClosedCases { get; set; } = new List<CaseGroupSummaryDto>();
        public Dictionary<string, int> AgingFromRequestedCounts { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> AgingInCurrentStatusCounts { get; set; } = new Dictionary<string, int>();
    }
}
