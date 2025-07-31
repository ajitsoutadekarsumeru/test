using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTriggerDto : DtoBridge
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string? TriggerTypeId { get; set; } // trigger type
        [Required]
        public string RecipientType { get; set; }
        public List<string> TriggerTemplates { get; set; }
    }
    

}
