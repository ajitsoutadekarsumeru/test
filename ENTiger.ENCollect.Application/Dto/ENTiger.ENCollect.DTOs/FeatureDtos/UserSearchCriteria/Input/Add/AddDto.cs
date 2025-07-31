using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    public partial class AddDto : DtoBridge
    {
        [Required]
        public string? FilterValues { get; set; }

        [Required]
        public string? FilterName { get; set; }

        [Required]
        public string? UseCaseName { get; set; }
    }
}