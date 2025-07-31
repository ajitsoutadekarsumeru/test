using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AddressDto : DtoBridge
    {
        public string? Id { get; set; }

        [Required]
        public string? AddressLine { get; set; }

        public string? LandMark { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        [Required]
        public string? PIN { get; set; }
    }
}