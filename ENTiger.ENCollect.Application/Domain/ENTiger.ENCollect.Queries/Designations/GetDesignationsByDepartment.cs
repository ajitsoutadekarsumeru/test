using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDesignationsByDepartment : FlexiQueryEnumerableBridgeAsync<Designation, GetDesignationsByDepartmentDto>
    {
        protected readonly ILogger<GetDesignationsByDepartment> _logger;
        protected GetDesignationsByDepartmentParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDesignationsByDepartment(ILogger<GetDesignationsByDepartment> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetDesignationsByDepartment AssignParameters(GetDesignationsByDepartmentParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetDesignationsByDepartmentDto>> Fetch()
        {
            return await Build<Designation>().SelectTo<GetDesignationsByDepartmentDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            if (string.IsNullOrEmpty(_params.DepartmentId))
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByDeleteDesignation();
                return query;
            }
            else
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .Where(a => a.DepartmentId == _params.DepartmentId)
                                        .ByDeleteDesignation();

                //Build Your Query Here

                return query;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetDesignationsByDepartmentParams : DtoBridge
    {
        public string? DepartmentId { get; set; }
    }
}