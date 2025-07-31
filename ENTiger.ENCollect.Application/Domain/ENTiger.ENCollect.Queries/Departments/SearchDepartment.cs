using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchDepartment : FlexiQueryEnumerableBridgeAsync<Department, SearchDepartmentDto>
    {
        protected readonly ILogger<SearchDepartment> _logger;
        protected SearchDepartmentParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchDepartment(ILogger<SearchDepartment> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchDepartment AssignParameters(SearchDepartmentParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchDepartmentDto>> Fetch()
        {
            var result = await Build<Department>().SelectTo<SearchDepartmentDto>().ToListAsync();
            return result.OrderByDescending(a => a.CreatedDate).ThenByDescending(b => b.LastModifiedDate);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByDepartmentNameSearch(_params.search)
                                    .ByDeleteDepartment();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchDepartmentParams : DtoBridge
    {
        public string search { get; set; }
    }
}