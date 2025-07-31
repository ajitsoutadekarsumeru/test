using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AckPayInSlipDto : DtoBridge
    {
        [Required]
        public List<string> payInSlipIds { get; set; }
    }
}