using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;
using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepoFactory _repoFactory;
        
        public UserRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }
        
        public async Task<List<string>> FindEligibleUserIds(
            FlexAppContextBridge flexContext, 
            ISearchCriteria aggregator, 
            IParameterContext? paramCtx, IContextDataPacket domainCtx)
        {
            _repoFactory.Init(flexContext);
            var expr = aggregator.Build<UserLevelProjection>();
            var result = _repoFactory.GetRepo().FindAll<UserLevelProjection>()
                            .Where(expr.Compile()).ToList()
                            .Select(x => x.ApplicationUserId).ToList();

            return result;
        }

        public async Task SaveAsync(FlexAppContextBridge context, UserLevelProjection userLevelProjection)
        {
            _repoFactory.Init(context);
            _repoFactory.GetRepo().InsertOrUpdate(userLevelProjection);
            await _repoFactory.GetRepo().SaveAsync();
        }
        
    }
}
