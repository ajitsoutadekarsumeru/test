using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyTypeDto : DtoBridge
    {
        public string? Id { get; set; }

        [Required]
        public string? MainType { get; set; }

        [Required]
        public string? SubType { get; set; }
    }
}