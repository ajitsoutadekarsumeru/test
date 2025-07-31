using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class SearchCompanyUser : FlexiQueryPagedListBridgeAsync<CompanyUser, SearchCompanyUserParams, SearchCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchCompanyUser> _logger;
        protected SearchCompanyUserParams _params;
        protected readonly IRepoFactory _repoFactory;
        private CompanyUserWorkflowState state;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCompanyUser(ILogger<SearchCompanyUser> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual SearchCompanyUser AssignParameters(SearchCompanyUserParams @params)
        {
            _params = @params;

            if (!string.IsNullOrEmpty(_params.Status))
            {
                state = WorkflowStateFactory.GetCompanyUserWFState(_params.Status);
            }
            return this;
        }

        /// <summary>
        /// Asynchronously returns a paginated list of agent data based on the provided search criteria
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchCompanyUserDto>> Fetch()
        {
            var projection = await Build<CompanyUser>().SelectTo<SearchCompanyUserDto>().ToListAsync();

            var result = BuildPagedOutput(projection);

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
                                    .IncludeWallet()
                                    .ByCompanyUserWFState(state)
                                    .ByCompanyUserPhone(_params.Phone)
                                    .ByDepartment(_params.DepartmentId)
                                    .ByDesignation(_params.RoleId)
                                    .ByCompanyUserFirstName(_params.Name)
                                    .OrderByDescending(i => i.CreatedDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    public class SearchCompanyUserParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Phone")]
        public string? Phone { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Name")]
        public string? Name { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid RoleId")]
        public string? RoleId { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid DepartmentId")]
        public string? DepartmentId { get; set; }

        //[Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? Status { get; set; }

        public int Take { get; set; }
    }
}