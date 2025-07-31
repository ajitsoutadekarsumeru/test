using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class AddCollectionBatchDto : DtoBridge
    {
        [Required]
        public ICollection<string> CollectionIds { get; set; }

        public decimal? Amount { get; set; }
        public string? ProductGroup { get; set; }
        public string? ModeOfPayment { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? BatchType { get; set; }
    }
}