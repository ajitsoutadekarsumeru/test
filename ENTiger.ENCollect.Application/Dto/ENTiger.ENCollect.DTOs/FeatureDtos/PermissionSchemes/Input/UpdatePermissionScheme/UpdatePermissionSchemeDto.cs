using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class UpdatePermissionSchemeDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Remarks { get; set; }
        public List<string> EnabledPermissionIds { get; set; }
    }

}
