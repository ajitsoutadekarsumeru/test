using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// Class responsible for searching and paginating permission schemes.
    /// </summary>
    public class SearchPermissions : FlexiQueryPagedListBridgeAsync<Permissions, SearchPermissionsParams, SearchPermissionsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchPermissions> _logger;
        protected SearchPermissionsParams _params;
        protected readonly RepoFactory _repoFactory;
        private readonly IPermissionSchemeRepository _permissionSchemeRepository;
        private readonly IDesignationRepository _designationRepository;
        /// <summary>
        /// Constructor for initializing PermissionSearchService.
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="repoFactory">Repository factory instance</param>
        /// <param name="permissionSchemeRepository">Permission scheme repository</param>
        public SearchPermissions(
            ILogger<SearchPermissions> logger,
            RepoFactory repoFactory,
            IPermissionSchemeRepository permissionSchemeRepository,
            IDesignationRepository designationRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _permissionSchemeRepository = permissionSchemeRepository;
            _designationRepository = designationRepository;
        }

        /// <summary>
        /// Sets the parameters for the search query.
        /// </summary>
        /// <param name="searchParams">Search query parameters</param>
        /// <returns>The current instance of PermissionSearchService for fluent chaining</returns>
        public virtual SearchPermissions AssignParameters(SearchPermissionsParams searchParams)
        {
            _params = searchParams;
            return this;
        }

        /// <summary>
        /// Fetches paginated results of permission schemes.
        /// </summary>
        /// <returns>Paginated list of SearchPermissionsDto</returns>
        public override async Task<FlexiPagedList<SearchPermissionsDto>> Fetch()
        {
            // Fetch the permissions along with their names and designations
            var permissionDtos = await Build<Permissions>()
                .SelectTo<SearchPermissionsDto>()
                .ToListAsync();

            // Get permission IDs from the fetched data
            var permissionIds = permissionDtos.Select(dto => dto.Id)
                                              .Distinct()
                                              .ToList();

            // Fetch related permission schemes based on the permission IDs
            var relatedPermissionSchemes = await _permissionSchemeRepository
                .GetEnabledPermissionsByPermissionIdsAsync(_params.GetAppContext(), permissionIds);

            var permissionSchemeIds = relatedPermissionSchemes.Select(dto => dto.PermissionScheme.Id)
                                            .Distinct()
                                            .ToList();
            // Fetch related designations or permissions if needed (example: from DesignationRepository or other sources)
            var permissionDesignationMappings = await _designationRepository
                .GetDesignationsByPermissionSchemeIdsAsync(_params.GetAppContext(), permissionSchemeIds);

            // Enhance the permissionDtos with Permission Name and Designation Names from related data
            foreach (var dto in permissionDtos)
            {
                // Fetch permission name based on the permission ID (if not already fetched)
                var permission = relatedPermissionSchemes.FirstOrDefault(p => p.PermissionId == dto.Id);
                if (permission != null)
                {
                    dto.PermissionSchemeName = permission.PermissionScheme.Name; // Assuming SchemeName maps to "Permission Name"
                }

                // Fetch designation names related to the permission
                var designations = permissionDesignationMappings
                    .Where(d => d.PermissionSchemeId == permission?.PermissionSchemeId)
                    .Select(d => d.Name)
                    .ToList();
                dto.DesignationNames = string.Join(", ", designations); // Assuming DesignationName is a list
            }

            // Build and return the final paged output
            var pagedResult = BuildPagedOutput(permissionDtos);

            return pagedResult;
        }


        /// <summary>
        /// Builds a query for a specified type, using parameters for paging.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>IQueryable for the specified entity</returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo()
                                              .FindAll<T>()
                                              .ByName(_params.Name);

            // Apply pagination and any other necessary query parameters
            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    /// <summary>
    /// Parameters for searching permissions, includes pagination support.
    /// </summary>
    public class SearchPermissionsParams : PagedQueryParamsDtoBridge
    {
        public string? Name { get; set; }
        public int Take { get; set; }
    }
}
