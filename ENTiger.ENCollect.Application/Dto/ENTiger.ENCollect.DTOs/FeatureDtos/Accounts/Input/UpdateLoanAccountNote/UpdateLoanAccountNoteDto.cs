using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateLoanAccountNoteDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }

        public string AccountId { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}