using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class BankDtoWithId : BankDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}