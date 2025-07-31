using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CompanyUserARMScopeOfWorkDtoWithId : CompanyUserARMScopeOfWorkDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}