using ENTiger.ENCollect.PermissionSchemesModule;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PermissionSchemeChangeLog : DomainModelBridge
    {
        /// <summary>
        /// Creates and populates a new permission scheme change log entry based on the provided command.
        /// </summary>
        /// <param name="cmd">The command containing change log data.</param>
        /// <returns>The populated <see cref="PermissionSchemeChangeLog"/> entity ready for persistence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the command is null.</exception>
        public virtual PermissionSchemeChangeLog CreatePermissionSchemeChangeLog(PermissionSchemeChangeLogDto cmd)
        {
            Guard.AgainstNull("PermissionSchemeAdded command cannot be empty", cmd);

            // Map fields from the DTO
            this.Convert(cmd);

            var appContext = cmd.GetAppContext();
            var userId = appContext?.UserId;

            this.CreatedBy = userId;
            this.LastModifiedBy = userId;

            // Map any other field not handled by Automapper config here

            // Mark entity as added and set timestamps
            this.SetAdded(SequentialGuid.NewGuidString());
            this.LastModifiedDate = DateTime.UtcNow;

            return this;
        }

    }
}
