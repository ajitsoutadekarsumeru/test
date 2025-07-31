using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CollectionDtoWithId : CollectionDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}