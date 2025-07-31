
namespace ENTiger.ENCollect
{
    public interface ILoanAccountQueryRepository
    {
        Task<int> GetValidAccountCount(List<string> accountIds, FlexAppContextBridge context);
        Task<string?> GetLoanAccountBranch(string accountId, FlexAppContextBridge context);
        Task<List<LoanAccount>> GetLoanAccountsByIdsAsync(List<string> ids, FlexAppContextBridge context);
        Task<LoanAccount> GetLoanAccountsByIdAsync(string id, FlexAppContextBridge context);
        Task<List<LoanAccountsProjection>> GetProjectionsByLoanAccountIdsAsync(List<string> loanAccountIds, FlexAppContextBridge context);
        Task SaveAsync(FlexAppContextBridge context, LoanAccount loanAccount);
        Task<List<string>> GetAccountIdsByPTPDateAsync(DateTime ptpDate, FlexAppContextBridge context);
        Task<List<string>> GetAccountIdsByBrokenPTPDateAsync( DateTime brokenPTPDate, FlexAppContextBridge context);
        Task<List<string>> GetAccountIdsByAgencyAllocationDateAsync(DateTime allocationDate);
        Task<List<string>> GetAccountIdsByXDaysPastDueDate(int daysOffSet, FlexAppContextBridge context);
        Task<List<string>> GetAccountIdsByXDaysBeforeDueDate(int daysOffSet, FlexAppContextBridge context);
        Task<List<string>> GetAccountIdsByXDaysAfterStatementDate(int daysOffSet, FlexAppContextBridge context);
    }
}
