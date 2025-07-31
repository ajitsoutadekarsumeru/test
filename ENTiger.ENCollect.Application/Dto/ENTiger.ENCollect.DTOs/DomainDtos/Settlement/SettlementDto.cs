using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SettlementDto : DtoBridge
    {
        public string? LoanAccountId { get; set; }
        public decimal SettlementAmount { get; set; }
        public string? CustomerName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? CustomId { get; set; }
        public string? AgreementId { get; set; }
    }

}
