namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionBatchesDto : DtoBridge
    {
        public PayInSlipCollectionBatchDto collectionBatch { get; set; }
        public ICollection<PayInSlipCollectionDetailsDto> collectionDetails { get; set; }
    }
}