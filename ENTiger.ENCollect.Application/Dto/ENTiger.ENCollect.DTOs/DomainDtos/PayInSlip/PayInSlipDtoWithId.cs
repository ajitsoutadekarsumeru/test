using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class PayInSlipDtoWithId : PayInSlipDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}