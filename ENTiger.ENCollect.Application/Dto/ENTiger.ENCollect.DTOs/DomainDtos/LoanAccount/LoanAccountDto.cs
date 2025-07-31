using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class LoanAccountDto : DtoBridge
    {
        [StringLength(100)]
        public string? CustomId { get; set; }

        public string? AGREEMENTID { get; set; }
        public string? CUSTOMERID { get; set; }
        public string? PRODUCT { get; set; }
        public string? ProductGroup { get; set; }
        public string? BRANCH { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public decimal? CURRENT_TOTAL_AMOUNT_DUE { get; set; }
        public DateTime? LatestPTPDate { get; set; }
        public string? ProductCode { get; set; }

    }
}