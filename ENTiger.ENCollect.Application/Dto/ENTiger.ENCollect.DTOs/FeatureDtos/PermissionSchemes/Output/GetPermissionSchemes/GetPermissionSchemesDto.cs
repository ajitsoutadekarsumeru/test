using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPermissionSchemesDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
