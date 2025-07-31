using ENTiger.ENCollect.PermissionsModule;
using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPermissionSchemeByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public List<EnabledPermissionSchemeDto> EnabledPermissions { get; set; }

        public List<GetPermissionSchemeChangeLogDto> PermissionSchemeChangeLogs { get; set; }

    }
}
