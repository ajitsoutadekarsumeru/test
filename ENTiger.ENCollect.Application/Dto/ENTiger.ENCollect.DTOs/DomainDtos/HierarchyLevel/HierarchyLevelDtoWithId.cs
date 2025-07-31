using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{

    public partial class HierarchyLevelDtoWithId : HierarchyLevelDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}
