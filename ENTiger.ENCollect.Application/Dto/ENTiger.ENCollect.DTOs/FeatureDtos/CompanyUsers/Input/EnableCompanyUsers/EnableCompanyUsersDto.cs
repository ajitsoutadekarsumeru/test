using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class EnableCompanyUsersDto : DtoBridge
    {
        [Required]
        public List<string> CompanyUserIds { get; set; }
        public string? Description { get; set; }
    }
}
