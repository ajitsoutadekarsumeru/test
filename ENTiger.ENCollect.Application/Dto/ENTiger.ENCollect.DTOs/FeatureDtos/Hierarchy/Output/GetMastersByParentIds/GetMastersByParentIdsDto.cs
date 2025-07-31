using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMastersByParentIdsDto : DtoBridge
    {
        public string Id { get; set; }
        public string LevelId { get; set; }
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
}
