using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetDesignationsWithSchemeDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? DepartmentName { get; set; }
        public string? PermissionSchemeName { get; set; }

            
    }
}
