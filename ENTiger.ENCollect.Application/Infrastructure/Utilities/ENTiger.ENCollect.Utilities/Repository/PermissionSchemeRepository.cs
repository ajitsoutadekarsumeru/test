using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Repository for accessing and managing permission schemes and related data.
    /// </summary>
    public class PermissionSchemeRepository : IPermissionSchemeRepository
    {
        private readonly IRepoFactory _repoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionSchemeRepository"/> class.
        /// </summary>
        /// <param name="repoFactory">An instance of <see cref="IRepoFactory"/> used to create repository instances.</param>
        public PermissionSchemeRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Retrieves a list of permission schemes matching a given scheme name.
        /// </summary>
        /// <param name="context">The FlexApp context bridge.</param>
        /// <param name="name">The name of the permission scheme to filter by.</param>
        /// <returns>A list of matching <see cref="PermissionSchemes"/>.</returns>
        public async Task<List<PermissionSchemes>> GetSchemeByNameAsync(FlexAppContextBridge context, string name)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                                     .FindAll<PermissionSchemes>()
                                     .ByPermissionSchemeName(name)
                                     .ToListAsync();
        }

        /// <summary>
        /// Retrieves the change logs for a specific permission scheme by its ID.
        /// </summary>
        /// <param name="context">The FlexApp context bridge.</param>
        /// <param name="permissionSchemeId">The ID of the permission scheme.</param>
        /// <returns>
        /// A list of <see cref="PermissionSchemeChangeLog"/> entries related to the specified permission scheme.
        /// </returns>
        public async Task<List<PermissionSchemeChangeLog>> GetChangeLogsBySchemeIdAsync(FlexAppContextBridge context, string permissionSchemeId)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                                     .FindAll<PermissionSchemeChangeLog>()
                                     .Where(log => log.PermissionSchemeId == permissionSchemeId)
                                     .ToListAsync();
        }

        /// <summary>
        /// Retrieves enabled permissions by a list of permission IDs.
        /// </summary>
        /// <param name="context">The FlexApp context bridge.</param>
        /// <param name="permissionIds">List of permission IDs.</param>
        /// <returns>
        /// A list of <see cref="EnabledPermission"/> entities related to the specified permission IDs.
        /// </returns>
        public async Task<List<EnabledPermission>> GetEnabledPermissionsByPermissionIdsAsync(FlexAppContextBridge context, List<string> permissionIds)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                                     .FindAll<EnabledPermission>()
                                     .FlexInclude(p => p.PermissionScheme)
                                     .ByPermissionIds(permissionIds)
                                     .ToListAsync();
        }
    }
}
