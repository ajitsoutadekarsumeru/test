using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class LoanAccountFlagDtoWithId : LoanAccountFlagDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}