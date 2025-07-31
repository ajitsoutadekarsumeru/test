using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserPerformanceBandMasterDtoWithId : UserPerformanceBandMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}