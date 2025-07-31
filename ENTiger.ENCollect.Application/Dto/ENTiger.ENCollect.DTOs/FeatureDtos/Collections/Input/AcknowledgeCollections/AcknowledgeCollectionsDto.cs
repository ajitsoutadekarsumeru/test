using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AcknowledgeCollectionsDto : DtoBridge
    {
        [Required]
        public ICollection<string> CollectionIds { get; set; }
    }
}