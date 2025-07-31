using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class CreatePayInSlipDto : DtoBridge
    {
        [Required]
        public string ProductGroup { get; set; }

        [Required]
        public List<string> BatchIds { get; set; }

        public string? CMSPayInSlipNo { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public DateTime? DateOfDeposit { get; set; }

        [Required]
        public string BankAccountNo { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public string ModeOfPayment { get; set; }

        public decimal Amount { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public string? DepositSlipImageName { get; set; }
        public string? PayinSlipType { get; set; }
    }
}