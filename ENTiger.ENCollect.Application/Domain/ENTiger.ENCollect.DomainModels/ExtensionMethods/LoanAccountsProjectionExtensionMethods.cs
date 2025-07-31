namespace ENTiger.ENCollect;
public static class LoanAccountsProjectionExtensionMethods
{

    public static IQueryable<T> ByTransactionMonthAndYear<T>(this IQueryable<T> model) where T : LoanAccountsProjection
    {
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        model = model.Where(a => a.Month == currentMonth && a.Year == currentYear);
        return model;
    }

    public static IQueryable<T> ByTransactionLoanAccountIds<T>(this IQueryable<T> query, List<string> accountIds) where T : LoanAccountsProjection
    {
        if (accountIds != null && accountIds.Any())
        {
            query = query.Where(t => accountIds.Contains(t.LoanAccountId));
        }
        return query;
    }

    public static IQueryable<T> ByProjectionLoanAccountId<T>(this IQueryable<T> query, string accountId) where T : LoanAccountsProjection
    {
        if (!string.IsNullOrEmpty(accountId))
        {
            query = query.Where(t => t.LoanAccountId == accountId);
        }
        return query;
    }
    public static IQueryable<T> ByBrokenPTPDate<T>(this IQueryable<T> account, DateTime? BrokenPTPDate) where T : LoanAccountsProjection
    {
        if (BrokenPTPDate.HasValue && BrokenPTPDate != DateTime.MinValue)
        {
            DateTime startDate = BrokenPTPDate.Value;
            DateTime endDate = startDate.AddDays(1);
            account=account.Where(a => a.LatestBPTPDate >= startDate && a.LatestBPTPDate < endDate);
        }
        return account;
    }
}
