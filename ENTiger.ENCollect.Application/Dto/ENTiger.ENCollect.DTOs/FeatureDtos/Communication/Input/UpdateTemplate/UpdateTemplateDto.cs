using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTemplateDto : DtoBridge, IValidatableObject
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string TemplateType { get; set; }
        public bool IsAvailableInAccountDetails { get; set; }

        [Required]
        public List<UpdateCommunicationTemplateDetailDto> CommunicationTemplateDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TemplateType == CommunicationTemplateTypeEnum.Email.DisplayName && CommunicationTemplateDetails.Where(w => string.IsNullOrEmpty(w.Subject)).Count() > 0)
            {
                yield return new ValidationResult("The Subject is required");
            }

            //Check whether the template type is available in the TemplateType enum
            var availableTemplateTypes = CommunicationTemplateTypeEnum.GetAll().Select(s => s.DisplayName).ToHashSet();
            if (!availableTemplateTypes.Contains(TemplateType, StringComparer.OrdinalIgnoreCase))
            {
                yield return new ValidationResult(
                    $"Invalid TemplateType '{TemplateType}'. Allowed values are: {string.Join(", ", availableTemplateTypes)}",
                    new[] { nameof(TemplateType) });
            }
        }
    }

    public class UpdateCommunicationTemplateDetailDto : DtoBridge
    {
        public string? Id { get; set; }

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