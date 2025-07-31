using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class DeactivateCompanyUserDto : DtoBridge
    {
        [Required]
        public List<string> companyUserIds { get; set; }
        [Required]
        public string? description { get; set; }
    }
}