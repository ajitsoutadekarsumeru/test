using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class CompanyUserDepartments : FlexiQueryEnumerableBridgeAsync<Department, CompanyUserDepartmentsDto>
    {
        protected readonly ILogger<CompanyUserDepartments> _logger;
        protected CompanyUserDepartmentsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public CompanyUserDepartments(ILogger<CompanyUserDepartments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual CompanyUserDepartments AssignParameters(CompanyUserDepartmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<CompanyUserDepartmentsDto>> Fetch()
        {
            var result = await Build<Department>().SelectTo<CompanyUserDepartmentsDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().CompanyUserDesignations(_params.IsFrontEndStaff);

            //Build Your Query Here
            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class CompanyUserDepartmentsParams : DtoBridge
    {
        public bool? IsFrontEndStaff { get; set; }
    }
}