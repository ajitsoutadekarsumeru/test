namespace ENTiger.ENCollect
{
    public interface ICollectionBatchPoster
    {
        Task PostCollectBatchAsync(CollectionBatchDtoWithId collectionBatch, string tenantId);

    }
}