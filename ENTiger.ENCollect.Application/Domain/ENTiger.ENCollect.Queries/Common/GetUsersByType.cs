using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUsersByType : FlexiQueryEnumerableBridgeAsync<ApplicationUser, GetUsersByTypeDto>
    {
        protected readonly ILogger<GetUsersByType> _logger;
        protected GetUsersByTypeParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUsersByType(ILogger<GetUsersByType> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUsersByType AssignParameters(GetUsersByTypeParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUsersByTypeDto>> Fetch()
        {
            List<GetUsersByTypeDto> outputModel = new List<GetUsersByTypeDto>();
            if (string.Equals(_params.UserType, "agencyuser", StringComparison.OrdinalIgnoreCase))
            {
                outputModel = await Build<AgencyUser>()
                    .IncludeAgencyUserDesignation()
                    .Where(x => x.Designation.Any(b => b.DepartmentId == _params.DepartmentId && b.DesignationId == _params.DesignationId) &&
                                x.AgencyUserWorkflowState != null &&
                                x.AgencyUserWorkflowState.Name.IndexOf("approved") >= 0)
                    .SelectTo<GetUsersByTypeDto>().OrderBy(a => a.FirstName)
                    .ToListAsync();

                //outputModel = await result.OrderBy(a => a.FirstName).ToListAsync();
            }
            else if (string.Equals(_params.UserType, "staff"))
            {
                outputModel = await Build<CompanyUser>()
                    .IncludeDesignation()
                    .Where(x => x.Designation.Any(b => b.DepartmentId == _params.DepartmentId && b.DesignationId == _params.DesignationId) &&
                                x.CompanyUserWorkflowState != null &&
                                x.CompanyUserWorkflowState.Name.IndexOf("approved") >= 0)
                    .SelectTo<GetUsersByTypeDto>().OrderBy(a => a.FirstName)
                    .ToListAsync();

                //outputModel = result.OrderBy(a => a.FirstName).ToListAsync();
            }

            return outputModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUsersByTypeParams : DtoBridge
    {
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public string DesignationId { get; set; }
        [Required]
        public string UserType { get; set; }
    }
}