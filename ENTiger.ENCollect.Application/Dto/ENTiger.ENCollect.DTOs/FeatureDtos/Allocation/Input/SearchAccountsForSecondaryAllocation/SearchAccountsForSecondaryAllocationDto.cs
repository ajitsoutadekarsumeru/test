using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SearchAccountsForSecondaryAllocationDto : DtoBridge
    {
        public string? ProductGroup { get; set; }

        public string? Product { get; set; }

        public string? SubProduct { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Bucket")]
        public string? Bucket { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid City")]
        public string? City { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid State")]
        public string? State { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Invalid Region")]
        public string? Region { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Zone")]
        public string? Zone { get; set; }

        public string? Branch { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid TeleCallingAgencyId")]
        public string? TeleCallingAgencyId { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid AgencyId")]
        public string? AgencyId { get; set; }

        public bool IsAllocated { get; set; }

        public bool IsUnAllocated { get; set; }

        public string? customId { get; set; }
        public DateTime? FromDate { get; set; }

        //[Required]
        public DateTime? ToDate { get; set; }

        public bool isloanaccount { get; set; }
    }
}