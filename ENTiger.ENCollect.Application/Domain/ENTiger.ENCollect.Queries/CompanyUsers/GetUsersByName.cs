using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class GetUsersByName : FlexiQueryEnumerableBridgeAsync<CompanyUser, GetUserByNameDto>
    {
        protected readonly ILogger<GetUsersByName> _logger;
        protected GetUsersByNameParams _params;
        protected readonly IRepoFactory _repoFactory;

        public GetUsersByName(ILogger<GetUsersByName> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }
        public virtual GetUsersByName AssignParameters(GetUsersByNameParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<IEnumerable<GetUserByNameDto>> Fetch()
        {
            return await Build<CompanyUser>().SelectTo<GetUserByNameDto>().ToListAsync();
        }
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByCompanyUserFirstName(_params.Name)
                                        .IncludeOnlyActiveUsers();
            return query;
        }
    }
    public class GetUsersByNameParams : DtoBridge
    {
        public string? Name { get; set; }
    }
}