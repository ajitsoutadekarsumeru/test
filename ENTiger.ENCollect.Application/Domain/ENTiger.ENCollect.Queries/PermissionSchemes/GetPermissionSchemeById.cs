using ENTiger.ENCollect.PermissionsModule;
using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Fetches Permission Scheme details by Scheme ID.
    /// </summary>
    public class GetPermissionSchemeById : FlexiQueryBridgeAsync<PermissionSchemes, GetPermissionSchemeByIdDto>
    {
        protected readonly ILogger<GetPermissionSchemeById> _logger;
        protected GetPermissionSchemeByIdParams? _params;
        protected readonly RepoFactory _repoFactory;

        private readonly IPermissionSchemeRepository _permissionSchemeRepository;
        private readonly IApplicationUserQueryRepository _applicationUserQueryRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPermissionSchemeById"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="repoFactory">Repository factory for database operations.</param>
        public GetPermissionSchemeById(ILogger<GetPermissionSchemeById> logger, RepoFactory repoFactory, IPermissionSchemeRepository permissionSchemeRepository, IApplicationUserQueryRepository applicationUserQueryRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _permissionSchemeRepository = permissionSchemeRepository;
            _applicationUserQueryRepository = applicationUserQueryRepository;
        }

        /// <summary>
        /// Assigns query parameters.
        /// </summary>
        /// <param name="params">Query parameters containing Scheme ID.</param>
        /// <returns>Current instance with assigned parameters.</returns>
        public virtual GetPermissionSchemeById AssignParameters(GetPermissionSchemeByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Fetches the Permission Scheme details by ID, including its associated change logs.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a <see cref="GetPermissionSchemeByIdDto"/> which includes
        /// the permission scheme details and its corresponding change logs.
        /// </returns>
        public override async Task<GetPermissionSchemeByIdDto> Fetch()
        {
            // 1. Fetch the permission scheme
            var result = await Build<PermissionSchemes>()
                                .SelectTo<GetPermissionSchemeByIdDto>()
                                .FirstOrDefaultAsync();


            // 2. Fetch change logs for the scheme
            var changeLogs = await _permissionSchemeRepository.GetChangeLogsBySchemeIdAsync(_params?.GetAppContext(), result.Id);

            // 3. Map change logs to DTOs
            result.PermissionSchemeChangeLogs = changeLogs.AsQueryable()
                                                         .SelectTo<GetPermissionSchemeChangeLogDto>()
                                                         .ToList();

            // 4. Extract distinct CreatedBy user IDs (non-null only)
            var userIds = result.PermissionSchemeChangeLogs.Select(x => x.CreatedBy).Distinct().ToList();

            // 5. Fetch users in batch
            var users = await _applicationUserQueryRepository.GetUsersByIdsAsync(_params?.GetAppContext(), userIds);

            // 6. Create a lookup dictionary for userId -> full name
            // Dictionary: userId -> "[CustomId] FirstName LastName"
            // Map userId → user
            var userDict = users.ToDictionary(u => u.Id, u => u);

            foreach (var log in result.PermissionSchemeChangeLogs)
            {
                if (log.CreatedBy != null && userDict.TryGetValue(log.CreatedBy, out var createdUser))
                {
                    log.CreatedBy = $"{(createdUser.FirstName ?? string.Empty)} {(createdUser.LastName ?? string.Empty)}".Trim();
                    log.UserId = createdUser.CustomId; // Assign to log.CustomId
                }
            }


            return result;
        }




        /// <summary>
        /// Builds the database query for fetching the Permission Scheme with included permissions.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>IQueryable representing the built query.</returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo()
                                              .FindAll<T>()
                                              .Where(t => t.Id == _params.Id);

            return query;
        }
    }


    /// <summary>
    /// Parameters required to fetch Permission Scheme by ID.
    /// </summary>
    public class GetPermissionSchemeByIdParams : DtoBridge
    {
        /// <summary>
        /// Unique identifier of the Permission Scheme.
        /// </summary>
        public string Id { get; set; }
    }
}
