namespace ENTiger.ENCollect
{
    /// <summary>
    /// Interface for repository operations related to designations.
    /// </summary>
    public interface IDesignationRepository
    {
        /// <summary>
        /// Asynchronously retrieves a list of designations associated with a specific permission scheme.
        /// </summary>
        /// <param name="context">The FlexApp database context bridge used to initialize the repository.</param>
        /// <param name="permissionSchemeId">The unique identifier for the permission scheme.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a list of <see cref="Designation"/> entities for the specified permission scheme.
        /// </returns>
        Task<List<Designation>> GetDesignationsBySchemeIdAsync(FlexAppContextBridge context, string permissionSchemeId);

        /// <summary>
        /// Asynchronously retrieves a list of designations associated with a list of permission scheme IDs.
        /// </summary>
        /// <param name="context">The FlexApp database context bridge used to initialize the repository.</param>
        /// <param name="permissionIds">A list of permission scheme IDs to filter the designations by.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The result contains a list of <see cref="Designation"/> entities for the specified permission scheme IDs.
        /// </returns>
        Task<List<Designation>> GetDesignationsByPermissionSchemeIdsAsync(FlexAppContextBridge context, List<string> permissionIds);
    }
}
