using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserAccessRightsDtoWithId : UserAccessRightsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}