using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class AccountScopeConfigurationQueryRepository : IAccountScopeConfigurationQueryRepository
    {
        private readonly IRepoFactory _repoFactory;

        public AccountScopeConfigurationQueryRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        public async Task<List<AccountScopeConfiguration>> GetScopeConfigurations(List<Accountability> accountabilities, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            
            // We extract the distinct accountability type IDs.
            var accountabilityTypeIds = accountabilities.Select(a => a.AccountabilityTypeId).Distinct().ToList();

            // Retrieve scope configurations that match the given accountability type IDs.
            return await _repoFactory.GetRepo().FindAll<AccountScopeConfiguration>()
                        .Where(sc => accountabilityTypeIds.Contains(sc.AccountabilityTypeId))
                        .ToListAsync();
        }
    }
}
