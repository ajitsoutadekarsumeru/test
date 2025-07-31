using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TreatmentOnCommunicationInputDto
    {
        public string? Id { get; set; }

        [StringLength(30)]
        public string? CommunicationType { get; set; }

        [StringLength(50)]
        public string? CommunicationTemplateId { get; set; }

        [StringLength(200)]
        public string? CommunicationMobileNumberType { get; set; }
    }
}