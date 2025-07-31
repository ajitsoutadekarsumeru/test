using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect;

public class LoanAccountQueryRepository : ILoanAccountQueryRepository
{
    private readonly IRepoFactory _repoFactory;

    public LoanAccountQueryRepository(IRepoFactory repoFactory)
    {
        _repoFactory = repoFactory;
    }
    public async Task<int> GetValidAccountCount(List<string> accountIds, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        int existingAccountCount = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .ByTFlexIds(accountIds)
                                                .CountAsync();
        return existingAccountCount;
    }

    public async Task<string?> GetLoanAccountBranch(string accountId, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        string? branch = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .ByTFlexId(accountId)
                                                .Select(s => s.BRANCH)
                                                .FirstOrDefaultAsync();
        return branch;
    }
    public async Task<List<LoanAccount>> GetLoanAccountsByIdsAsync(List<string> ids, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo()
            .FindAll<LoanAccount>()
            .ByAccountIds(ids)
            .ToListAsync();
    }
    public async Task<LoanAccount> GetLoanAccountsByIdAsync(string id, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo()
            .FindAll<LoanAccount>()
            .ByAccountId(id)
            .FirstOrDefaultAsync();
    }
    public async Task<List<LoanAccountsProjection>> GetProjectionsByLoanAccountIdsAsync(List<string> loanAccountIds, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo().FindAll<LoanAccountsProjection>()
            .ByTransactionLoanAccountIds(loanAccountIds)
            .ByTransactionMonthAndYear()
            .Include(p => p.LoanAccount)
            .ToListAsync();
    }
    public async Task SaveAsync(FlexAppContextBridge context, LoanAccount loanAccount)
    {
        _repoFactory.Init(context);
        _repoFactory.GetRepo().InsertOrUpdate(loanAccount);
        await _repoFactory.GetRepo().SaveAsync();
    }

    public Task<List<string>> GetAccountIdsByTodaysPtpDateAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetAccountIdsByPTPDateAsync( DateTime ptpDate, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo()
            .FindAll<LoanAccount>()
            .ByPTPDate(ptpDate)
            .Select(x => x.Id) // Select only AccountId
            .ToListAsync();
    }

    public async Task<List<string>> GetAccountIdsByBrokenPTPDateAsync(DateTime brokenPTPDate, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo().FindAll<LoanAccountsProjection>()
            .ByBrokenPTPDate(brokenPTPDate)
            .Select(x => x.LoanAccountId) // Select only AccountId
            .ToListAsync();
    }

    public Task<List<string>> GetAccountIdsByAgencyAllocationDateAsync(DateTime allocationDate)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetAccountIdsByXDaysPastDueDate(int daysOffSet, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo().FindAll<LoanAccount>()
            .ByPastDueDate(daysOffSet)
            .Select(x => x.Id) // Select only AccountId
            .ToListAsync();
    }

    public async Task<List<string>> GetAccountIdsByXDaysBeforeDueDate(int daysOffSet, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo().FindAll<LoanAccount>()
            .ByBeforeDueDate(daysOffSet)
            .Select(x => x.Id) // Select only AccountId
            .ToListAsync();
    }

    public async Task<List<string>> GetAccountIdsByXDaysAfterStatementDate(int daysOffSet, FlexAppContextBridge context)
    {
        _repoFactory.Init(context);

        return await _repoFactory.GetRepo()
            .FindAll<LoanAccount>()
            .ByAfterStatementDate(daysOffSet)
            .Select(x => x.Id) // Select only AccountId
            .ToListAsync();
    }
}
