using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly IRepoFactory _repoFactory;

        public CollectionRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }
        public async Task<List<Collection>> GetCollectionsByIdsAsync(List<string> ids, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                .FindAll<Collection>()
                .ByCollectionIds(ids)
                .IncludeAccount()
                .ToListAsync();
        }
        public async Task<List<Collection>> GetCollectionsByBatchIdsAsync(List<string> batchIds, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                .FindAll<Collection>()
                .ByCollectionBatchIds(batchIds)
                .ToListAsync();
        }
        /// <summary>
        /// Retrieves the list of collection IDs for the given date where the payment mode is 'Online'
        /// and the workflow state is 'Initiated'.
        /// </summary>
        /// <param name="date">The date to filter collection records by.</param>
        /// <param name="context">The Flex application context used for tenant-specific access.</param>
        /// <returns>List of collection IDs as strings.</returns>
        public async Task<List<string>> GetOnlineCollectionIdsByDateAsync(DateTime date, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);

            var state = new CollectionInitiated();

            return await _repoFactory.GetRepo()
                .FindAll<Collection>()
                .ByCollectionPaymentMode(CollectionModeEnum.Online.Value)
                .OnDate(date)
                .ByCollectionWorkflowState(state)
                .Select(c => c.Id)
                .ToListAsync();
        }


    }
}
