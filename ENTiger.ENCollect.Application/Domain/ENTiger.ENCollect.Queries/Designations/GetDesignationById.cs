using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDesignationById : FlexiQueryBridgeAsync<Designation, GetDesignationByIdDto>
    {
        protected readonly ILogger<GetDesignationById> _logger;
        protected GetDesignationByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDesignationById(ILogger<GetDesignationById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetDesignationById AssignParameters(GetDesignationByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetDesignationByIdDto> Fetch()
        {
            // var result = Build<Designation>().SelectTo<GetDesignationByIdDto>().FirstOrDefaultAsync();

            var result = await Build<Designation>().Select(x => new GetDesignationByIdDto()
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.Name,
                DesignationTypeId = x.DesignationTypeId,
                DesignationTypeName = x.DesignationType.Name,
                DesignationAcronym = x.Acronym,
                ReportsTo = x.ReportsToDesignation,
                Level = x.Level,
                // CreatedBy = CoreUtilities.GetApplicationUserName(x.CreatedBy, ),
                //CreatedDate = x.CreatedDate.DateTime,
                //LastModifiedBy = CoreUtilities.GetApplicationUserName(x.LastModifiedBy, TenantId),
                //LastModifiedDate = x.LastModifiedDate.DateTime,
                //AccountabilityTypeId = "BankTo" + x.Department.DepartmentType + x.DesignationTypeId
            }).FirstOrDefaultAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByDeleteDesignation().Where(t => t.Id == _params.Id);
            return query;
        }
    }

    public class GetDesignationByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}