using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public class MySettlementQueueDetailsDto : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
        public string LoanAccountId { get; set; }
        public string NpaFlag { get; set; }
       
        public string Status { get; set; }
        public decimal SettlementAmount { get; set; }
        public decimal? RenegotiationAmount { get; set; }
        public decimal PrincipalWaiverAmount { get; set; }
        public decimal PrincipalWaiverPercentage { get; set; }
        public decimal InterestWaiverAmount { get; set; }
        public decimal InterestWaiverPercentage { get; set; }
        public ActionedBeforeDto ActionedBeforeYou { get; set; }
        public int AgingInCurrentStatusDays { get; set; }

        public string WorkflowName { get; set; }
        public string WorkflowInstanceId { get; set; }
        public string? StepName { get; set; }
        public string? StepType { get; set; }
    }

    public class ActionedBeforeDto : DtoBridge
    {
        public int Count { get; set; }
        public List<string> Names { get; set; }
    }
}
