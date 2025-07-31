using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;

public static class SearchCriteriaExtensionsHelper
{
    /// <summary>
    /// Adds the given filter definition to the SearchCriteria,
    /// using .And(...) if this is the first filter, otherwise .Or(...).
    /// 
    /// The <paramref name="firstFilter"/> is a reference to a bool 
    /// that indicates whether we've already added something to 'sc'.
    /// After adding this filter, we set <paramref name="firstFilter"/> to false.
    /// </summary>
    public static void AddOrCondition(
        this SearchCriteria sc, 
        FilterDefinition newFilter, 
        ref bool firstFilter)
    {
        if (firstFilter)
        {
            sc.And(newFilter);
            firstFilter = false;
        }
        else
        {
            sc.Or(newFilter);
        }
    }
}