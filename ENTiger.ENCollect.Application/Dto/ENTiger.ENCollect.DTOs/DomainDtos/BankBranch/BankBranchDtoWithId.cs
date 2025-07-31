using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class BankBranchDtoWithId : BankBranchDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}