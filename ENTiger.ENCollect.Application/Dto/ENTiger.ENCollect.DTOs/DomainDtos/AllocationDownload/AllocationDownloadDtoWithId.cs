using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AllocationDownloadDtoWithId : AllocationDownloadDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}