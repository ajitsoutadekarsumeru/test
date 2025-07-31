using System.ComponentModel.DataAnnotations;
namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class AddGeoMasterDto : DtoBridge
    {
        [Required]
        public string LevelId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z0-9_ ]*$", ErrorMessage = "Invalid item")]
        public string Item { get; set; }

        [Required]
        public string ParentId { get; set; }
    }
}
