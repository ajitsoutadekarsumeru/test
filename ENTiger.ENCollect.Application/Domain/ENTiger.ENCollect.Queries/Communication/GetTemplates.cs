using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class GetTemplates : FlexiQueryEnumerableBridgeAsync<CommunicationTemplate, GetTemplatesDto>
    {
        
        protected readonly ILogger<GetTemplates> _logger;
        protected GetTemplatesParams _params;
        protected readonly RepoFactory _repoFactory;

        public GetTemplates(ILogger<GetTemplates> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual GetTemplates AssignParameters(GetTemplatesParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<IEnumerable<GetTemplatesDto>> Fetch()
        {
            var result = await Build<CommunicationTemplate>().SelectTo<GetTemplatesDto>().ToListAsync();

            return result;
        }

        protected override IQueryable<T> Build<T>()
        {
           _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByTemplateType(_params.TemplateType)
                                        .ByEntryPoint(_params.EntryPoint)
                                        .ByRecipientType(_params.RecipientType)
                                        .ByActiveTemplate();

            return query;
        }
    }

    public class GetTemplatesParams : DtoBridge, IValidatableObject
    {
        [Required]
        public string TemplateType { get; set; }
        [Required]
        public string EntryPoint { get; set; }
        [Required]
        public string RecipientType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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
}
