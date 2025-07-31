using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class DormantCompanyUserDto : DtoBridge
    {
        [Required]
        public List<string> companyUserIds { get; set; }
    }
}