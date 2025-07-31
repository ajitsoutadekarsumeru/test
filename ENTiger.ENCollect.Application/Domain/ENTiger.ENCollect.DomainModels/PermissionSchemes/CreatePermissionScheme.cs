using Sumeru.Flex;
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
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual PermissionSchemes CreatePermissionScheme(CreatePermissionSchemeCommand cmd)
        {
            Guard.AgainstNull("PermissionSchemes command cannot be empty", cmd);

            this.Convert(cmd.Dto);

            var appContext = cmd.Dto.GetAppContext();
            this.CreatedBy = appContext?.UserId;
            this.LastModifiedBy = appContext?.UserId;

            this.SetAdded(cmd.Dto.GetGeneratedId());

            // 🟢 Map EnabledPermissions based on EnabledPermissionIds
            if (cmd.Dto.EnabledPermissionIds?.Any() == true)
            {
                this.EnabledPermissions = cmd.Dto.EnabledPermissionIds.Select(permissionId => new EnabledPermission
                {
                    PermissionId = permissionId,
                    PermissionSchemeId = this.Id, // will link back after save

                }).ToList();
                // SetAdded on each EnabledPermission
                foreach (var enabledPermission in this.EnabledPermissions)
                {
                    enabledPermission.SetCreatedBy(appContext?.UserId);
                    enabledPermission.SetLastModifiedBy(appContext?.UserId);
                    enabledPermission.SetAdded();
                }
            }

            return this;
        }

        #endregion


        #region "Private Methods"
        #endregion

    }
}
