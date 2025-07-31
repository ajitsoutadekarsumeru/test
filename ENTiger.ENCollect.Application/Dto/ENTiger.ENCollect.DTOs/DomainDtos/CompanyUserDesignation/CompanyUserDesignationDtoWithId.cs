using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CompanyUserDesignationDtoWithId : CompanyUserDesignationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}