using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CategoryItemDtoWithId : CategoryItemDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}