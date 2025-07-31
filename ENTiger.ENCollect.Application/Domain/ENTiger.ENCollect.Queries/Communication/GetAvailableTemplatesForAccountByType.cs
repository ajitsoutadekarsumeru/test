using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAvailableTemplatesForAccountByType : FlexiQueryEnumerableBridge<CommunicationTemplate, GetAvailableTemplatesForAccountByTypeDto>
    {
        
        protected readonly ILogger<GetAvailableTemplatesForAccountByType> _logger;
        protected GetAvailableTemplatesForAccountByTypeParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAvailableTemplatesForAccountByType(ILogger<GetAvailableTemplatesForAccountByType> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAvailableTemplatesForAccountByType AssignParameters(GetAvailableTemplatesForAccountByTypeParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<GetAvailableTemplatesForAccountByTypeDto> Fetch()
        {
            var result = Build<CommunicationTemplate>().SelectTo<GetAvailableTemplatesForAccountByTypeDto>().ToList();

            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
           _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByAvailAccountsTemplate()
                                    .ByTemplateType(_params.TemplateType);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetAvailableTemplatesForAccountByTypeParams : DtoBridge, IValidatableObject
    {
        public string? TemplateType { get; set; }
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
