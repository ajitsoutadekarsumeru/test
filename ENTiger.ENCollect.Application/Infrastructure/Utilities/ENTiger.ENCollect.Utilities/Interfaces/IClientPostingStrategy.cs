namespace ENTiger.ENCollect
{
    public interface IClientPostingStrategy
    {
        Task PostCollectionAsync(CollectionDtoWithId collection);
        Task PostCollectBatchAsync(CollectionBatchDtoWithId collectionBatch, List<FeatureMasterDtoWithId> paymentDetails, string tenantId);
        Task PostPayInSlipAsync(PayInSlipDtoWithId payinSlip);

    }
}
