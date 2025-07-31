using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTemplateByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public string TemplateType { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public bool IsAvailableInAccountDetails { get; set; }
        public string EntryPoint { get; set; }
        public string RecipientType { get; set; }
        public List<GetCommunicationTemplateDetailsDto> CommunicationTemplateDetails { get; set; }
    }
}