using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CollectionBatchDtoWithId : CollectionBatchDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}