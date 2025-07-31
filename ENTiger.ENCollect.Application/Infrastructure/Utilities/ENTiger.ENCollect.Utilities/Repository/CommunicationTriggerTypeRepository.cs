using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class TriggerTypeRepository : ITriggerTypeRepository
    {
        private readonly IRepoFactory _repoFactory;

        public TriggerTypeRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        

        public async Task<TriggerType?> GetByTypeIdAsync(
            FlexAppContextBridge context, string id)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
            .FindAll<TriggerType>()
                            .Where(w => w.Id == id)
                            .Include(x => x.Triggers)
                            .ThenInclude(t => t.TriggerTemplates)
                            .FirstOrDefaultAsync();
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
    }
}
