using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class UpdateUsersByBatchDto : DtoBridge
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
    }
}