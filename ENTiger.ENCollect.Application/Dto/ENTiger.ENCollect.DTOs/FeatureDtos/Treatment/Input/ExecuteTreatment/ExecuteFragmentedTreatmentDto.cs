using ENTiger.ENCollect.TreatmentModule;
using System.Data;

namespace ENTiger.ENCollect
{
    public class ExecuteFragmentedTreatmentDto : DtoBridge
    {
        public List<ElasticSearchSimulateLoanAccountDto> loanAccounts;

        public List<string> segments { get; set; }

        public DateTime? treatExecutionStartdate { get; set; }

        public DateTime? treatExecutionEnddate { get; set; }

        public string? TreatmentHistoryId { get; set; }

        public string? TreatmentId { get; set; }

        public DataTable LoanAccountTable { get; set; }

        public int FinalCountOfAccounts { get; set; }

        public int totalCountOfAccounts { get; set; }

        public string? PartyId { get; set; }

        public string? WorkRequestId { get; set; }

        public string? TenantId { get; set; }

        public ExecuteTreatmentDto executeTreatmentDto { get; set; }
    }
}