namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTemplateDetailsByTemplateIds : FlexiQueryEnumerableBridge<CommunicationTemplate, GetTemplateDetailsByTemplateIdsDto>
    {
        
        protected readonly ILogger<GetTemplateDetailsByTemplateIds> _logger;
        protected GetTemplateDetailsByTemplateIdsParams _params;
        protected readonly RepoFactory _repoFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetTemplateDetailsByTemplateIds(ILogger<GetTemplateDetailsByTemplateIds> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTemplateDetailsByTemplateIds AssignParameters(GetTemplateDetailsByTemplateIdsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<GetTemplateDetailsByTemplateIdsDto> Fetch()
        {
            var result = Build<CommunicationTemplate>().SelectTo<GetTemplateDetailsByTemplateIdsDto>().ToList();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByTemplateIds(_params.TemplateIds);
            return query;
        }

      
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetTemplateDetailsByTemplateIdsParams : DtoBridge
    {
        public List<string> TemplateIds { get; set; }
    }
}
