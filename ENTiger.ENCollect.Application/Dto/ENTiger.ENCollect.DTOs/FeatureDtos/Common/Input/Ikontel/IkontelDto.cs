using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class IkontelDto : DtoBridge
    {
        [Required]
        public string accountno { get; set; }

        [Required]
        public string MobileNumberAparty { get; set; }

        [Required]
        public string MobileNumberBparty { get; set; }
    }
}