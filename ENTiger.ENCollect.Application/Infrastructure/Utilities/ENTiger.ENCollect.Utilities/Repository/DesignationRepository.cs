using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Repository for accessing designation-related data operations.
    /// </summary>
    public class DesignationRepository : IDesignationRepository
    {
        private readonly IRepoFactory _repoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignationRepository"/> class.
        /// </summary>
        /// <param name="repoFactory">The repository factory for data access.</param>
        public DesignationRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Gets the list of designations associated with a specific permission scheme.
        /// </summary>
        /// <param name="context">The context containing user/session/environment details.</param>
        /// <param name="permissionSchemeId">The unique identifier for the permission scheme.</param>
        /// <returns>A list of <see cref="Designation"/> entities for the specified permission scheme.</returns>
        public async Task<List<Designation>> GetDesignationsBySchemeIdAsync(FlexAppContextBridge context, string permissionSchemeId)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                .FindAll<Designation>()
                .FlexInclude(d => d.Department)
                .Where(d => d.PermissionSchemeId == permissionSchemeId)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the list of designations associated with multiple permission scheme IDs.
        /// </summary>
        /// <param name="context">The context containing user/session/environment details.</param>
        /// <param name="permissionIds">A list of permission scheme identifiers.</param>
        /// <returns>A list of <see cref="Designation"/> entities matching the given permission scheme IDs.</returns>
        public async Task<List<Designation>> GetDesignationsByPermissionSchemeIdsAsync(FlexAppContextBridge context, List<string> permissionIds)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                .FindAll<Designation>()
                .FlexInclude(d => d.Department)
                .ByPermissionSchemeIds(permissionIds)
                .ToListAsync();
        }
    }
}
