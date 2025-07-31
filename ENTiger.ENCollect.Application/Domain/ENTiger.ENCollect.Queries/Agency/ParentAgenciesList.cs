using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class ParentAgenciesList : FlexiQueryEnumerableBridgeAsync<Agency, ParentAgenciesListDto>
    {
        protected readonly ILogger<ParentAgenciesList> _logger;
        protected ParentAgenciesListParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public ParentAgenciesList(ILogger<ParentAgenciesList> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual ParentAgenciesList AssignParameters(ParentAgenciesListParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<ParentAgenciesListDto>> Fetch()
        {
            var result = await Build<Agency>().SelectTo<ParentAgenciesListDto>().ToListAsync();

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
                                   .ByParent();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ParentAgenciesListParams : DtoBridge
    {
    }
}