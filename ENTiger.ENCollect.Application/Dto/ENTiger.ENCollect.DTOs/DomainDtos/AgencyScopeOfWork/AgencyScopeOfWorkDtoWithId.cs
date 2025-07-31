using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyScopeOfWorkDtoWithId : AgencyScopeOfWorkDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}