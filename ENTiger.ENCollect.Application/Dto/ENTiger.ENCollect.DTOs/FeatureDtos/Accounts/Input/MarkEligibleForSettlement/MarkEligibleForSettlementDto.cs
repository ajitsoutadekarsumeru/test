using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class MarkEligibleForSettlementDto : DtoBridge
    {
        [Required]
        public List<string> LoanAccountIds { get; set; }
    }
}
