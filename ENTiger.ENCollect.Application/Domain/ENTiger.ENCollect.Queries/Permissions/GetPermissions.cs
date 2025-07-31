using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// Handles the logic to fetch permissions and return them as DTOs.
    /// </summary>
    public class GetPermissions : FlexiQueryEnumerableBridgeAsync<Permissions, GetPermissionsDto>
    {
        protected readonly ILogger<GetPermissions> _logger;
        protected readonly RepoFactory _repoFactory;
        protected GetPermissionsParams _params;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPermissions"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for diagnostic purposes.</param>
        /// <param name="repoFactory">Factory for accessing repositories.</param>
        public GetPermissions(ILogger<GetPermissions> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Assigns input parameters to be used while fetching data.
        /// </summary>
        /// <param name="params">The filter or configuration parameters.</param>
        /// <returns>Current instance of <see cref="GetPermissions"/> with parameters assigned.</returns>
        public virtual GetPermissions AssignParameters(GetPermissionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Asynchronously fetches permissions based on the constructed query.
        /// </summary>
        /// <returns>A list of permissions as <see cref="GetPermissionsDto"/>.</returns>
        public override async Task<IEnumerable<GetPermissionsDto>> Fetch()
        {
            var result = await Build<Permissions>()
                .SelectTo<GetPermissionsDto>()
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Constructs the query to fetch permission entities. Can be customized further.
        /// </summary>
        /// <typeparam name="T">The entity type to query.</typeparam>
        /// <returns>An <see cref="IQueryable{T}"/> for further filtering or projection.</returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory
                .GetRepo()
                .FindAll<T>();

            // Additional filtering or includes can be applied here.

            return query;
        }
    }

    /// <summary>
    /// Represents the input parameters required to fetch permissions.
    /// Extend this class with properties to support filtering.
    /// </summary>
    public class GetPermissionsParams : DtoBridge
    {
        // Example: Add filtering properties like RoleId, ModuleName etc.
    }
}
