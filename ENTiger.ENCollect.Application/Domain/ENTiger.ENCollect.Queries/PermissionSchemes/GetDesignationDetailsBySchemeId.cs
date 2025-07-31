using ENTiger.ENCollect.DesignationsModule;
using ENTiger.ENCollect.PermissionsModule;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Fetches designation details linked to a specific permission scheme ID.
    /// Inherits from FlexiQueryBridgeAsync to enable async querying and mapping.
    /// </summary>
    public class GetDesignationDetailsBySchemeId : FlexiQueryBridgeAsync<PermissionSchemes, GetDesignationDetailsBySchemeIdDto>
    {
        /// <summary>
        /// Logger instance for diagnostic and operational logging.
        /// </summary>
        protected readonly ILogger<GetDesignationDetailsBySchemeId> _logger;

        /// <summary>
        /// Holds the parameters assigned for this query execution.
        /// </summary>
        protected GetDesignationDetailsBySchemeIdParams _params;

        /// <summary>
        /// Repository factory used for database access.
        /// </summary>
        protected readonly RepoFactory _repoFactory;

        private readonly IDesignationRepository _designationRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDesignationDetailsBySchemeId"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        /// <param name="repoFactory">Repository factory for database operations.</param>
        public GetDesignationDetailsBySchemeId(ILogger<GetDesignationDetailsBySchemeId> logger, RepoFactory repoFactory, IDesignationRepository designationRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _designationRepository = designationRepository;
        }

        /// <summary>
        /// Assigns the query parameters before executing the fetch operation.
        /// </summary>
        /// <param name="params">Query parameters including the permission scheme ID.</param>
        /// <returns>The current instance with assigned parameters.</returns>
        public virtual GetDesignationDetailsBySchemeId AssignParameters(GetDesignationDetailsBySchemeIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Fetches designation details along with related designations based on permission scheme ID.
        /// </summary>
        /// <returns>A DTO containing permission scheme and associated designations.</returns>
        public override async Task<GetDesignationDetailsBySchemeIdDto> Fetch()
        {
            // Fetch the permission scheme details
            var result = await Build<PermissionSchemes>().SelectTo<GetDesignationDetailsBySchemeIdDto>().FirstOrDefaultAsync();

            // Fetch all designations mapped to the permission scheme
            // Fetch designations mapped to the permission scheme using the repository
            var designations = await _designationRepository.GetDesignationsBySchemeIdAsync(_params?.GetAppContext(), result.Id);

            // Assign designations to the result            
            result.Designations = designations.AsQueryable()
                                                          .SelectTo<GetDesignationByPermissionSchemeIdDto>()
                                                          .ToList();
            return result;
        }

        /// <summary>
        /// Builds the base query filtered by the permission scheme ID.
        /// </summary>
        /// <typeparam name="T">Entity type to query.</typeparam>
        /// <returns>IQueryable of filtered entity records.</returns>
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
    /// Parameters DTO used to pass the scheme ID for fetching designation details.
    /// </summary>
    public class GetDesignationDetailsBySchemeIdParams : DtoBridge
    {
        /// <summary>
        /// Gets or sets the unique ID of the permission scheme.
        /// </summary>
        public string Id { get; set; }
    }
}
