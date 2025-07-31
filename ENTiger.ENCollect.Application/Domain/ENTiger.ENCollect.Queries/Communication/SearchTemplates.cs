using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class SearchTemplates : FlexiQueryPagedListBridgeAsync<CommunicationTemplate, SearchTemplatesParams,SearchTemplatesDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchTemplates> _logger;
        protected SearchTemplatesParams _params;
        protected readonly IRepoFactory _repoFactory;

        public SearchTemplates(ILogger<SearchTemplates> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual SearchTemplates AssignParameters(SearchTemplatesParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<FlexiPagedList<SearchTemplatesDto>> Fetch()
        {            
            var projection = await Build<CommunicationTemplate>().SelectTo<SearchTemplatesDto>().ToListAsync();

            var result = BuildPagedOutput(projection);

            return result;
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByTemplateName(_params.TemplateName)
                                    .ByTemplateType(_params.TemplateType)
                                    .IncludeTemplateDetails()
                                    .IncludeTemplateTriggers()
                                    .OrderByDescending(a => a.LastModifiedDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    public class SearchTemplatesParams : PagedQueryParamsDtoBridge, IValidatableObject
    {
        [RegularExpression("^[a-zA-Z0-9_ ]*$", ErrorMessage = "Invalid Name")]
        public string? TemplateName { get; set; }
        public string? TemplateType { get; set; }
        public int skip { get; set; }
        public int take { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Check whether the template type is available in the TemplateType enum
            var availableTemplateTypes = CommunicationTemplateTypeEnum.GetAll().Select(s => s.DisplayName).ToHashSet();
            if (TemplateType != null && !availableTemplateTypes.Contains(TemplateType, StringComparer.OrdinalIgnoreCase))
            {
                yield return new ValidationResult(
                    $"Invalid TemplateType '{TemplateType}'. Allowed values are: {string.Join(", ", availableTemplateTypes)}",
                    new[] { nameof(TemplateType) });
            }
        }
    }
}