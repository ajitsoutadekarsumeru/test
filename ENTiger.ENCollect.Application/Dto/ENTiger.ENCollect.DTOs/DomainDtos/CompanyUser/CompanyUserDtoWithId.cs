using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CompanyUserDtoWithId : CompanyUserDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}