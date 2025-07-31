using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CommonModule
{
    public class GetTriggerTypes : FlexiQueryEnumerableBridgeAsync<TriggerType, GetTriggerTypesDto>
    {
        
        protected readonly ILogger<GetTriggerTypes> _logger;
        protected GetTriggerTypesParams _params;
        protected readonly RepoFactory _repoFactory;

        public GetTriggerTypes(ILogger<GetTriggerTypes> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual GetTriggerTypes AssignParameters(GetTriggerTypesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// fetch the trigger types
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTriggerTypesDto>> Fetch()
        {
            var result = await Build<TriggerType>().SelectTo<GetTriggerTypesDto>().ToListAsync();

            return result;
        }
        
        /// <summary>
        /// build the query for teching active trigger types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
           _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .Where(w => w.IsActive);

            return query;
        }
    }

    public class GetTriggerTypesParams : DtoBridge
    {

    }
}
