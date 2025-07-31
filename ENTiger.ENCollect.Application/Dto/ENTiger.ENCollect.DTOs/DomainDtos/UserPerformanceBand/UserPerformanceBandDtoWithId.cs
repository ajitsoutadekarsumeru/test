using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserPerformanceBandDtoWithId : UserPerformanceBandDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}