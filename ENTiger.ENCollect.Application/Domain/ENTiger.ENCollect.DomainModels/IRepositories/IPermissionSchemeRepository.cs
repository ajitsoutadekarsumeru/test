namespace ENTiger.ENCollect
{
    /// <summary>
    /// Interface for repository operations related to permission schemes and their associated data.
    /// </summary>
    public interface IPermissionSchemeRepository
    {
        /// <summary>
        /// Retrieves permission schemes that match the provided scheme name.
        /// </summary>
        /// <param name="context">The FlexApp database context bridge used to initialize the repository.</param>
        /// <param name="name">The name of the permission scheme to filter by.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a list of <see cref="PermissionSchemes"/> objects that match the provided name.
        /// </returns>
        Task<List<PermissionSchemes>> GetSchemeByNameAsync(FlexAppContextBridge context, string name);

        /// <summary>
        /// Asynchronously retrieves all change logs associated with the specified permission scheme ID.
        /// </summary>
        /// <param name="context">The FlexApp database context bridge used to initialize the repository.</param>
        /// <param name="permissionSchemeId">The unique identifier of the permission scheme.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a list of <see cref="PermissionSchemeChangeLog"/> objects corresponding to the specified permission scheme ID.
        /// </returns>
        Task<List<PermissionSchemeChangeLog>> GetChangeLogsBySchemeIdAsync(FlexAppContextBridge context, string permissionSchemeId);

        /// <summary>
        /// Retrieves a list of enabled permissions based on the provided list of permission IDs.
        /// </summary>
        /// <param name="context">The FlexApp database context bridge used to initialize the repository.</param>
        /// <param name="permissionIds">A list of permission IDs to filter by.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a list of <see cref="EnabledPermission"/> objects that match the provided permission IDs.
        /// </returns>
        Task<List<EnabledPermission>> GetEnabledPermissionsByPermissionIdsAsync(FlexAppContextBridge context, List<string> permissionIds);
    }
}
