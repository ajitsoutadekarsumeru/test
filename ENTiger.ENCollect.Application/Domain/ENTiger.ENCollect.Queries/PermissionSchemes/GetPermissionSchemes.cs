using ENTiger.ENCollect;
using Microsoft.EntityFrameworkCore;


namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Fetches a list of permission schemes with associated user details.
    /// </summary>
    public class GetPermissionSchemes : FlexiQueryEnumerableBridgeAsync<PermissionSchemes, GetPermissionSchemesDto>
    {
        protected readonly ILogger<GetPermissionSchemes> _logger;
        protected GetPermissionSchemesParams _params;
        protected readonly RepoFactory _repoFactory;
        private readonly IApplicationUserQueryRepository _applicationUserQueryRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPermissionSchemes"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="repoFactory">Repository factory for database operations.</param>
        public GetPermissionSchemes(ILogger<GetPermissionSchemes> logger, RepoFactory repoFactory, IApplicationUserQueryRepository applicationUserQueryRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _applicationUserQueryRepository = applicationUserQueryRepository;
        }

        /// <summary>
        /// Assigns query parameters for fetching permission schemes.
        /// </summary>
        /// <param name="params">Query parameters.</param>
        /// <returns>The current instance with assigned parameters.</returns>
        public virtual GetPermissionSchemes AssignParameters(GetPermissionSchemesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Fetches permission schemes and populates created by user names.
        /// </summary>
        /// <returns>List of permission schemes with user display names.</returns>
        public override async Task<IEnumerable<GetPermissionSchemesDto>> Fetch()
        {
            // 1. Fetch permission schemes
            var result = await Build<PermissionSchemes>()
                                .SelectTo<GetPermissionSchemesDto>()
                                .ToListAsync();

            // 2. Extract distinct CreatedBy user IDs
            var userIds = result.Select(x => x.CreatedBy).Distinct().ToList();

            // 3. Fetch users in one batch using application user query repository
            var users = await _applicationUserQueryRepository.GetUsersByIdsAsync(_params?.GetAppContext(), userIds);

            // 4. Create a lookup dictionary (userId -> full name)
            var userDict = users.ToDictionary(
                u => u.Id,
                u => $"{u.FirstName ?? ""} {u.LastName ?? ""}".Trim()
            );

            // 5. Map full names to the DTOs
            result.ForEach(a =>
            {
                if (a.CreatedBy != null && userDict.ContainsKey(a.CreatedBy))
                {
                    a.CreatedBy = userDict[a.CreatedBy];
                }
            });

            return result;
        }


        /// <summary>
        /// Builds the base query for fetching permission schemes.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>IQueryable instance for building LINQ queries.</returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            // Additional filters or joins can be applied here

            return query;
        }
    }
}


/// <summary>
/// 
/// </summary>
public class GetPermissionSchemesParams : DtoBridge
{

}

