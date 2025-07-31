using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class MastersImportDto : DtoBridge
    {
        [Required]
        public string? FileName { get; set; }

        [Required]
        public string? FileType { get; set; }

        public string? CustomId { get; set; }
    }
}