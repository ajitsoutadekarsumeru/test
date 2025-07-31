using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class BaseBranchDtoWithId : BaseBranchDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}