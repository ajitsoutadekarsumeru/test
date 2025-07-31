using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDepartmentById : FlexiQueryBridgeAsync<Department, GetDepartmentByIdDto>
    {
        protected readonly ILogger<GetDepartmentById> _logger;
        protected GetDepartmentByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDepartmentById(ILogger<GetDepartmentById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetDepartmentById AssignParameters(GetDepartmentByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetDepartmentByIdDto> Fetch()
        {
            return await Build<Department>().SelectTo<GetDepartmentByIdDto>().FirstOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByDeleteDepartment().Where(t => t.Id == _params.Id);
            return query;
        }
    }

    public class GetDepartmentByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}