using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class GetTemplateById : FlexiQueryBridgeAsync<CommunicationTemplate, GetTemplateByIdDto>
    {
        protected readonly ILogger<GetTemplateById> _logger;
        protected GetTemplateByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        public GetTemplateById(ILogger<GetTemplateById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual GetTemplateById AssignParameters(GetTemplateByIdParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<GetTemplateByIdDto> Fetch()
        {
            var result = await Build<CommunicationTemplate>()
                                    .SelectTo<GetTemplateByIdDto>()
                                    .FirstOrDefaultAsync();

            return result;
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .IncludeTemplateDetails()
                                        .ByTFlexId(_params.Id);
            return query;
        }
    }

    public class GetTemplateByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}