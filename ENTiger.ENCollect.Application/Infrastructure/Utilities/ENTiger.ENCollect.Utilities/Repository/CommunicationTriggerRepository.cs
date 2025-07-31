using Elastic.Clients.Elasticsearch;
using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CommunicationTriggerRepository : ICommunicationTriggerRepository
    {
        private readonly IRepoFactory _repoFactory;

        public CommunicationTriggerRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }


        public async Task SaveTriggerAsync(FlexAppContextBridge context, CommunicationTrigger trigger)
        {
            _repoFactory.Init(context);
            _repoFactory.GetRepo().InsertOrUpdate(trigger);
            await _repoFactory.GetRepo().SaveAsync();
        }

        public async Task SaveAsync(FlexAppContextBridge context, TriggerType triggerType)
        {
            _repoFactory.Init(context);
            _repoFactory.GetRepo().InsertOrUpdate(triggerType);
            await _repoFactory.GetRepo().SaveAsync();
        }

        public async Task EnqueueRangeAsync(FlexAppContextBridge context, string runId, string triggerId, IReadOnlyList<string> accountIds)
        {
            _repoFactory.Init(context);

            foreach (var accountId in accountIds)
            {
                var entity = new TriggerAccountQueueProjection(triggerId, runId, accountId);

                _repoFactory.GetRepo().InsertOrUpdate(entity);
            }

            await _repoFactory.GetRepo().SaveAsync();
        }

        public async Task<IEnumerable<CommunicationTrigger>> GetAllActiveAsync(FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
            .FindAll<CommunicationTrigger>()
                            .ByActiveCommunicationTrigger()
                            .Include(x=>x.TriggerType)
                            .ToListAsync();
        }
        public async Task<List<TriggerDeliverySpec>> GetDeliverySpecsAsync(string triggerId, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                .FindAll<CommunicationTrigger>()
                .Include(a => a.TriggerTemplates)
                .Where(t => t.Id == triggerId && t.IsDeleted == false)
                .Select(b => b.TriggerTemplates)
                .FirstOrDefaultAsync();
        }
    }
}
