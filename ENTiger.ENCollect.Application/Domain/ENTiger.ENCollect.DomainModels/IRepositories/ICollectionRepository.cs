namespace ENTiger.ENCollect
{
    public interface ICollectionRepository
    {
        Task<List<Collection>> GetCollectionsByIdsAsync(List<string> ids, FlexAppContextBridge context);
        Task<List<Collection>> GetCollectionsByBatchIdsAsync(List<string> batchIds, FlexAppContextBridge context);
        Task<List<string>> GetOnlineCollectionIdsByDateAsync(DateTime date, FlexAppContextBridge context);

    }
}
