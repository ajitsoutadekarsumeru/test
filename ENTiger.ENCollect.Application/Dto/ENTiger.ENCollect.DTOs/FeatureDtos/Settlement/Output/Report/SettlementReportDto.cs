using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public class SettlementReportDto : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
        public string LoanAccountId { get; set; }
        public string RequestorId { get; set; }
        public string NpaFlag { get; set; }

        public string Status { get; set; }
        public decimal SettlementAmount { get; set; }
        public decimal PrincipalWaiverAmount { get; set; }
        public decimal PrincipalWaiverPercentage { get; set; }
        public decimal InterestWaiverAmount { get; set; }
        public decimal InterestWaiverPercentage { get; set; }
       
        public int AgingInCurrentStatusDays { get; set; }
        public int AgingSinceRequestedDays { get; set; }

       
       
        public DateTimeOffset CreatedDate { get; set; }
        public DateTime StatusUpdatedOn { get; set; }
    }
}
