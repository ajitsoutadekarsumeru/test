using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class ApplicationUserDtoWithId : ApplicationUserDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}