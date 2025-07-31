using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class ApplicationUserDto : DtoBridge
    {
        [StringLength(100)]
        public string? CustomId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PrimaryMobileNumber { get; set; }
    }
}