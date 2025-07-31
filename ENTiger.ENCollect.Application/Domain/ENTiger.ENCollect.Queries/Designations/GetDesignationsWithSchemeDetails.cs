using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// Handles fetching of designations along with associated scheme and department details.
    /// </summary>
    public class GetDesignationsWithSchemeDetails
        : FlexiQueryEnumerableBridgeAsync<Designation, GetDesignationsWithSchemeDetailsDto>
    {
        protected readonly ILogger<GetDesignationsWithSchemeDetails> _logger;
        protected readonly RepoFactory _repoFactory;
        protected GetDesignationsWithSchemeDetailsParams _params;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDesignationsWithSchemeDetails"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging operations.</param>
        /// <param name="repoFactory">Factory to retrieve repositories for database operations.</param>
        public GetDesignationsWithSchemeDetails(
            ILogger<GetDesignationsWithSchemeDetails> logger,
            RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Assigns parameters required for fetching designations.
        /// </summary>
        /// <param name="params">The input parameters.</param>
        /// <returns>The current instance with assigned parameters.</returns>
        public virtual GetDesignationsWithSchemeDetails AssignParameters(GetDesignationsWithSchemeDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Asynchronously fetches the list of designations with related scheme and department details.
        /// </summary>
        /// <returns>A list of designation DTOs.</returns>
        public override async Task<IEnumerable<GetDesignationsWithSchemeDetailsDto>> Fetch()
        {
            var result = await Build<Designation>()
                .SelectTo<GetDesignationsWithSchemeDetailsDto>()
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Builds the base query with required includes based on the assigned parameters.
        /// </summary>
        /// <typeparam name="T">The type of entity to query.</typeparam>
        /// <returns>An IQueryable representing the query.</returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo()
                .FindAll<T>()
                .FlexInclude(entity => entity.Department)
                .FlexInclude(entity => entity.PermissionScheme);

            // Additional query logic can be applied here if needed

            return query;
        }
    }

    /// <summary>
    /// Represents the parameter object used to fetch designations with scheme details.
    /// </summary>
    public class GetDesignationsWithSchemeDetailsParams : DtoBridge
    {
        // Add parameter properties here if required
    }
}
