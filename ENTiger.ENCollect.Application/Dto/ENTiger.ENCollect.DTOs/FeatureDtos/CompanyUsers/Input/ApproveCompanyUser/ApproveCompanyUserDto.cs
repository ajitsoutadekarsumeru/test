using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ApproveCompanyUserDto : DtoBridge
    {
        [Required]
        public List<string> companyUserIds { get; set; }

        public string? description { get; set; }
    }
}