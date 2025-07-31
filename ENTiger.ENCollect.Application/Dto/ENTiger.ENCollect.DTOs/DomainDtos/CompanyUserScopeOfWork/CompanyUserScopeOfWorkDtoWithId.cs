using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CompanyUserScopeOfWorkDtoWithId : CompanyUserScopeOfWorkDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}