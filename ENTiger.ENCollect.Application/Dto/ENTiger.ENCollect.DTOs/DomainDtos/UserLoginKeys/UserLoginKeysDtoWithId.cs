using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserLoginKeysDtoWithId : UserLoginKeysDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}