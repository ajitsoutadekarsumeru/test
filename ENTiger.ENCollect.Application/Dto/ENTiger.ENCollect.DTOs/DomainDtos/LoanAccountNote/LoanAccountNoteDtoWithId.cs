using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class LoanAccountNoteDtoWithId : LoanAccountNoteDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}