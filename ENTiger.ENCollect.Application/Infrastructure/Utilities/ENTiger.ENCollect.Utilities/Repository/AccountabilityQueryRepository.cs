using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect;
public class AccountabilityQueryRepository : IAccountabilityQueryRepository
{
    private readonly IRepoFactory _repoFactory;

    public AccountabilityQueryRepository(IRepoFactory repoFactory)
    {
        _repoFactory = repoFactory;
    }


    public async Task<List<Accountability>> GetAccountabilities(string userId, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        // Retrieve all accountabilities for the given userId.
        return await _repoFactory.GetRepo().FindAll<Accountability>()
                            .Where(a => a.ResponsibleId == userId)
                            .ToListAsync();
    }
}


