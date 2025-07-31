using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class AcknowledgeCollectionBatchDto : DtoBridge
    {
        [Required]
        public string batchId { get; set; }
    }
}