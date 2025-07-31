using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountNoteDto : DtoBridge
    {
        [StringLength(50)]
        public string AccountId { get; set; }

        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}