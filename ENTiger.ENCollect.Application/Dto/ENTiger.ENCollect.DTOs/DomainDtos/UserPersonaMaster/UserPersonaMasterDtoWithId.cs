using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserPersonaMasterDtoWithId : UserPersonaMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}