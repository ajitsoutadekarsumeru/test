using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class DeleteTemplateDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}