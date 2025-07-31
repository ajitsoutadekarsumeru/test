using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CashDtoWithId : CashDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}