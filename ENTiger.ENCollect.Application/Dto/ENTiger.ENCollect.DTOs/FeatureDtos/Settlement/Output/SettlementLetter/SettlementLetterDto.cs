using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public class SettlementLetterDto : DtoBridge
    {
        public string? Id { get; set; }     
        public string? CustomId { get; set; }
        public string? LoanAccountId { get; set; }
        public string? CustomerName { get; set; }
        public string? MailingAddress { get; set; }
        public string? AgreementId { get; set; }
        public DateTime? SanctionDate { get; set; }
        public string? BankName { get; set; }
        public DateTimeOffset? ApprovalDate { get; set; }
        public string? DisbursedAmount { get; set; }
        public string? OutstandingDues { get; set; }
        public decimal? SettlementAmount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public int? InstallmentCount { get; set; }
    }
}
