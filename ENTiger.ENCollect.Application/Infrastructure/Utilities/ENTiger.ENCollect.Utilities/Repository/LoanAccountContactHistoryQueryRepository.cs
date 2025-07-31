using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect;

public class LoanAccountContactHistoryQueryRepository : ILoanAccountContactHistoryQueryRepository
{
    private readonly IRepoFactory _repoFactory;

    public LoanAccountContactHistoryQueryRepository(IRepoFactory repoFactory)
    {
        _repoFactory = repoFactory;
    }

    public async Task<bool> GetAccountContactHistoryExistsAsync(ContactSourceEnum source, ContactTypeEnum contactType, string contactValue, string accountId, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo()
           .FindAll<AccountContactHistory>()
           .ByContactSource(source)
           .ByContactType(contactType)
           .ByContactValue(contactValue)
           .ByContactAccountId(accountId)
           .AnyAsync();
    }
}
