using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class searchAgent : FlexiQueryPagedListBridgeAsync<AgencyUser, searchAgentParams, searchAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<searchAgent> _logger;
        protected searchAgentParams _params;
        protected readonly IRepoFactory _repoFactory;
        private AgencyUserWorkflowState state;
        protected string? _userId;
        private string? agencyId = string.Empty;
        private ApplicationUser? user = new ApplicationUser();
        private string? agencyname = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public searchAgent(ILogger<searchAgent> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual searchAgent AssignParameters(searchAgentParams @params)
        {
            _params = @params;
            state = WorkflowStateFactory.GetCollectionAgencyUserWFState(_params.Status);
            return this;
        }

        /// <summary>
        /// Asynchronously returns a paginated list of agent data based on the provided search criteria
        /// </summary>
        /// <returns></returns>user
        public override async Task<FlexiPagedList<searchAgentDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _userId = _params.GetAppContext()?.UserId;

            user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == _userId).FirstOrDefaultAsync();
            if (user?.GetType() == typeof(AgencyUser))
            {
                AgencyUser? agencyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                                    .IncludeAgencyUserAgency()
                                                    .Where(a=>a.Id == _userId)
                                                    .FirstOrDefaultAsync();                
                agencyId = agencyUser?.Agency?.Id;
            }
            else
            {
                agencyname = _params.AgencyName;
            }

            var projection = await Build<AgencyUser>().SelectTo<searchAgentDto>().ToListAsync();

            var departments = await _repoFactory.GetRepo().FindAll<Department>().ToListAsync();
            var designations = await _repoFactory.GetRepo().FindAll<Designation>().ToListAsync();
            foreach (var agent in projection)
            {
                foreach (var designation in agent.Roles)
                {
                    var design = designations.Where(x => x.Id == designation.DesignationId).FirstOrDefault();
                    if (design != null)
                    {
                        designation.DesignationId = design.Name;
                    }
                    var dept = departments.Where(x => x.Id == designation.DepartmentId).FirstOrDefault();
                    if (dept != null)
                    {
                        designation.DepartmentId = dept.Name;
                    }
                }
            }
            var result = BuildPagedOutput(projection);

            return result;
        }

        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .IncludeWallet()
                                    .ByWorkflowState(state)
                                    .ByAgentPhoneNo(_params.PhoneNo)
                                    .ByCardExpiryDate(_params.CardExpiryDate)
                                    .ByAgencyUserDesignation(_params.RoleId)
                                    .ByCollectionAgencyName(agencyname)
                                    .ByAgencyId(agencyId,user)
                                    .ByAgentName(_params.Name)
                                    .OrderByDescending(i => i.CreatedDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    public class searchAgentParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid PhoneNo")]
        public string? PhoneNo { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Name")]
        public string? Name { get; set; }

        public string? AgencyName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid RoleId")]
        public string? RoleId { get; set; }

        public DateTime? CardExpiryDate { get; set; }

        public int take { get; set; }

        public int skip { get; set; }
    }
}