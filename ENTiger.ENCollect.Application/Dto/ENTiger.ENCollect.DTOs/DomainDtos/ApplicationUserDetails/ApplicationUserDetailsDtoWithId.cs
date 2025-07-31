using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class ApplicationUserDetailsDtoWithId : ApplicationUserDetailsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}