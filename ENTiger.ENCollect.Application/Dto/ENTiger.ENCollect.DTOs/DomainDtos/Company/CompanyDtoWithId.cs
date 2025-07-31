using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CompanyDtoWithId : CompanyDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}