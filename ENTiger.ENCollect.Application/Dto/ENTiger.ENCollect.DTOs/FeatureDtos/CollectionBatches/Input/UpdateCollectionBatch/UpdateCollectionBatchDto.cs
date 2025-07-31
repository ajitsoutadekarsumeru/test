namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class UpdateCollectionBatchDto : DtoBridge
    {
        public string? BatchId { get; set; }
        public List<string> CollectionIds { get; set; }
    }
}