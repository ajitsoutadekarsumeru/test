using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountFlagDto : DtoBridge
    {
        [StringLength(50)]
        public string AccountId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        public bool? IsActive { get; set; }
    }
}