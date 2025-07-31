using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class CreateDepositSlipDto : DtoBridge
    {
        [Required]
        public string CMSPayInSlipNo { get; set; }

        [Required]
        public List<string> ReceiptIds { get; set; }

        [Required]
        public DateTime DateOfDeposit { get; set; }

        [Required]
        public string ModeOfPayment { get; set; }

        public string? Id { get; set; }
        public decimal Amount { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? AccountHolderName { get; set; }
        public string? PayInSlipImageName { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? Lattitude { get; set; }
        public string? ProductGroup { get; set; }
    }
}