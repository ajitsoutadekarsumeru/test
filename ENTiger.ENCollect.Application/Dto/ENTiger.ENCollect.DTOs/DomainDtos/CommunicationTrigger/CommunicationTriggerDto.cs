using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CommunicationTriggerDto : DtoBridge
    {
        [StringLength(32)]
        public string CommunicationTriggerId { get; set; }
        [Required]
        [StringLength(32)]
        public string CommunicationTemplateId { get; set; }
    }

}
