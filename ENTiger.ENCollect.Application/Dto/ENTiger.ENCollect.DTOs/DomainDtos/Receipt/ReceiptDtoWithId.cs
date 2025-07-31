using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class ReceiptDtoWithId : ReceiptDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}