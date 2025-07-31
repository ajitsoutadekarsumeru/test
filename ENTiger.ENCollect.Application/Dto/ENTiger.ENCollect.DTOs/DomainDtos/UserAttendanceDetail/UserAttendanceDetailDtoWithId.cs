using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserAttendanceDetailDtoWithId : UserAttendanceDetailDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}