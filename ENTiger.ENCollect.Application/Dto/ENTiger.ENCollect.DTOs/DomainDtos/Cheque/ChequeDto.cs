using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class ChequeDto : DtoBridge
    {
        [StringLength(100)]
        public string? BankName { get; set; }

        [StringLength(100)]
        public string? BranchName { get; set; }

        [StringLength(50)]
        public string? InstrumentNo { get; set; }

        public DateTime? InstrumentDate { get; set; }

        [StringLength(50)]
        public string? MICRCode { get; set; }

        [StringLength(50)]
        public string? IFSCCode { get; set; }

        [StringLength(50)]
        public string? BankCity { get; set; }
    }
}