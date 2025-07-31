using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyUserDtoWithId : AgencyUserDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}