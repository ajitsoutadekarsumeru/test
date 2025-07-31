using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CommunicationTemplateDetailDto : DtoBridge
    {

        [Required]
        [StringLength(50)]
        public string Language { get; set; }

        [StringLength(200)]
        public string? Subject { get; set; }
        
        [Required]
        [StringLength(5000)]
        public string Body { get; set; }
    }
}