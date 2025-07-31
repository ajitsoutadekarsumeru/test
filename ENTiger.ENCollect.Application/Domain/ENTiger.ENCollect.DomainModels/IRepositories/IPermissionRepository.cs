namespace ENTiger.ENCollect
{
    /// <summary>
    /// Interface for repository operations related to permissions.
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Retrieves a list of permissions based on the provided list of permission IDs.
        /// </summary>
        /// <param name="context">The FlexApp database context bridge used to initialize the repository.</param>
        /// <param name="permissionIds">A list of permission IDs to filter by.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a list of <see cref="Permissions"/> objects that match the provided permission IDs.
        /// </returns>
        Task<List<Permissions>> GetPermissionsByIdsAsync(FlexAppContextBridge context, List<string> permissionIds);
    }
}
