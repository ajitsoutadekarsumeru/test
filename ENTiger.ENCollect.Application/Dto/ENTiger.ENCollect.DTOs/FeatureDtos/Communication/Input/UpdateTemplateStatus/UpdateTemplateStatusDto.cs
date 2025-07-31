using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTemplateStatusDto : DtoBridge
    {
        [Required]
        public string TemplateId { get; set; }

        [Required]
        public bool IsDisabled { get; set; }
    }
}