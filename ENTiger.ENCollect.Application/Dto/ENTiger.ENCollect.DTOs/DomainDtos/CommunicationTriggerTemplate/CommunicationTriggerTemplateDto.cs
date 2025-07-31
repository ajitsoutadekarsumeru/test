using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CommunicationTriggerTemplateDto : DtoBridge
    {
        [Required]
        [StringLength(32)]
        public string CommunicationTemplateId { get; set; }
    }

}
