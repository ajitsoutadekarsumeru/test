using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateLoanAccountFlagDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }

        public string AccountId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}