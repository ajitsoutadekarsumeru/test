using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class AddTriggerDto : DtoBridge
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        public string? TriggerTypeId { get; set; } // trigger type
        public int DaysOffset { get; set; } // Used for XDays* condition types
        public string? Description { get; set; }
        public List<string> TriggerTemplates { get; set; }
        [Required]
        public string RecipientType { get; set; }
    }

}
