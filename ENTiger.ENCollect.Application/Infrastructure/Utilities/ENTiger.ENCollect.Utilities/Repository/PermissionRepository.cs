using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Repository for accessing permission-related data operations.
    /// </summary>
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IRepoFactory _repoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRepository"/> class.
        /// </summary>
        /// <param name="repoFactory">An instance of <see cref="IRepoFactory"/> used to create repository instances.</param>
        public PermissionRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Retrieves a list of permissions by their IDs.
        /// </summary>
        /// <param name="context">The context containing session/environment-specific data.</param>
        /// <param name="permissionIds">A list of permission IDs to fetch.</param>
        /// <returns>A list of <see cref="Permissions"/> entities corresponding to the provided IDs.</returns>
        public async Task<List<Permissions>> GetPermissionsByIdsAsync(FlexAppContextBridge context, List<string> permissionIds)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                                     .FindAll<Permissions>()
                                     .ByIds(permissionIds)
                                     .ToListAsync();
        }
    }
}
