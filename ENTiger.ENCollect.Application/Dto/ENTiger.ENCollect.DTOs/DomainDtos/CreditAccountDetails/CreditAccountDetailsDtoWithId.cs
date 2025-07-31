using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CreditAccountDetailsDtoWithId : CreditAccountDetailsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}