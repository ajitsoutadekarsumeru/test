using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect;
public class DynamicLoanAccountFieldFetcherService
{
   
    private static readonly Dictionary<string, Delegate> _cache = new();
    private readonly IRepoFactory _repoFactory;
    public DynamicLoanAccountFieldFetcherService(IRepoFactory repoFactory)
    {
        _repoFactory = repoFactory;
    }

    public async Task<Dictionary<string, object?>> FetchFieldsAsync(
        Expression<Func<LoanAccount, bool>> predicate,
        List<string> fields)
    {
        IQueryable<LoanAccount> query = _repoFactory.GetRepo().FindAll<LoanAccount>().AsQueryable();
        //_context.Set<T>();

        query = IncludeNestedFields(query, fields);

        var entity = await query.FirstOrDefaultAsync(predicate);
        if (entity == null)
            throw new Exception($"LoanAccount data not found.");

        var result = new Dictionary<string, object?>();
        result["Id"] = entity.Id; // Always include Id
        foreach (var field in fields)
        {
            var accessor = GetAccessor(field);
            result[field] = accessor(entity);
        }

        return result;
    }

    private IQueryable<LoanAccount> IncludeNestedFields(IQueryable<LoanAccount> query, List<string> fields)
    {
        var navigations = fields
            .Where(f => f.Contains('.'))
            .Select(f => f.Substring(0, f.LastIndexOf('.')))
            .Distinct();

        foreach (var navigation in navigations)
            query = query.Include(navigation);

        return query;
    }

    private Func<LoanAccount, object?> GetAccessor(string field)
    {
        if (_cache.TryGetValue(field, out var accessor))
            return (Func<LoanAccount, object?>)accessor;

        var parameter = Expression.Parameter(typeof(LoanAccount), "x");
        Expression currentExpr = parameter;

        foreach (var member in field.Split('.'))
        {
            var property = Expression.PropertyOrField(currentExpr, member);
            currentExpr = property;
        }

        var converted = Expression.Convert(currentExpr, typeof(object));
        var lambda = Expression.Lambda<Func<LoanAccount, object?>>(converted, parameter).Compile();

        _cache[field] = lambda;
        return lambda;
    }
}
