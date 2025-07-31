using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UsersUpdateFileDtoWithId : UsersUpdateFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}