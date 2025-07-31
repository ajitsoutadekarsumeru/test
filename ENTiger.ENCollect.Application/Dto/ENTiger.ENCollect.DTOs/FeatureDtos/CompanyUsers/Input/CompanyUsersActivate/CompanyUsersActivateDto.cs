using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersActivateDto : DtoBridge
    {
        [Required]
        public List<string> Ids { get; set; }
    }
}
