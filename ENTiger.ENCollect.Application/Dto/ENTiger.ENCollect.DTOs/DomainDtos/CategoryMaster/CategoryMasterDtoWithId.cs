using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CategoryMasterDtoWithId : CategoryMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}