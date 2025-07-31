using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountContactDetailsDto : DtoBridge
    {
        [StringLength(32)]
        public string? AccountId { get; set; }

        public string? LatestMobileNo { get; set; }
    }
}