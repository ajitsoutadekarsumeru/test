using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class UpdateACMDto : DtoBridge
    {
        [Required]
        public string Accountability { get; set; }

        public List<SearchACMDto> Details { get; set; }
    }
}