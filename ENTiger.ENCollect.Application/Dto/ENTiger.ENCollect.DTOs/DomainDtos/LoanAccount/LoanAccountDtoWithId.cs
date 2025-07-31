using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class LoanAccountDtoWithId : LoanAccountDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}