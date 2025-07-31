using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTriggerStatusDto : DtoBridge
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? TriggerTypeId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

}
