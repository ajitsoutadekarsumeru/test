using ENTiger.ENCollect.PermissionSchemesModule;
namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionSchemes : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual PermissionSchemes UpdatePermissionScheme(UpdatePermissionSchemeCommand cmd)
        {
            Guard.AgainstNull("PermissionSchemes model cannot be empty", cmd);
            this.Convert(cmd.Dto);

            var appContext = cmd.Dto.GetAppContext();
            var userId = appContext?.UserId;

            this.LastModifiedBy = userId;

            // Step 1: Handle Permission Updates (Remove old + Add latest)
            var newPermissionIds = cmd.Dto.EnabledPermissionIds?.ToList() ?? new List<string>();

            // Get old permission IDs
            var oldPermissionIds = this.EnabledPermissions?.Select(p => p.PermissionId).ToList() ?? new List<string>();

            // Step 2: Determine removed permissions (exist in DB but not in new list)
            var removedPermissions = this.EnabledPermissions?
                .Where(p => !newPermissionIds.Contains(p.PermissionId))
                .ToList() ?? new List<EnabledPermission>();

            foreach (var removed in removedPermissions)
            {
                removed.SetDeleted();
                removed.SetLastModifiedBy(userId);
            }

            // Step 3: Determine new permissions to be added (not already present)
            var permissionsToAdd = newPermissionIds.Except(oldPermissionIds);

            var newEnabledPermissions = permissionsToAdd
                .Select(pid =>
                {
                    var enabledPerm = new EnabledPermission
                    {
                        PermissionSchemeId = this.Id,
                        PermissionId = pid
                    };
                    enabledPerm.SetCreatedBy(userId);
                    enabledPerm.SetLastModifiedBy(userId);
                    enabledPerm.SetAdded();
                    return enabledPerm;
                }).ToList();

            // Step 4: Merge existing (unchanged) + new permissions
            var updatedPermissions = this.EnabledPermissions?
                .Where(p => newPermissionIds.Contains(p.PermissionId))
                .ToList() ?? new List<EnabledPermission>();

            updatedPermissions.AddRange(newEnabledPermissions);
            this.EnabledPermissions = updatedPermissions;

            // Step 5: Mark scheme as modified
            this.SetModified();

            return this;
        }


        #endregion

        #region "Private Methods"
        #endregion

    }
}
