namespace ENCollect.Dyna.Filters;

internal enum LogicConnector
{
    And,
    Or
}

/// <summary>
/// Aggregates one or more <see cref="IFilterDefinition"/> items (single-value operators only),
/// controlling AND/OR/NOT logic. Also supports placeholders (via <see cref="SetParameterContext(IParameterContext)"/>)
/// and merging with another criteria via <see cref="AndCriteria(ISearchCriteria)"/>.
/// (Multi‑value operators have been removed for simplicity.)
/// </summary>
public class SearchCriteria : ISearchCriteria
{
    private readonly List<(IFilterDefinition filter, LogicConnector connector, bool negate)> _items = new();
    private IParameterContext? _paramContext;

    /// <summary>
    /// Read-only view of the internal filter collection.
    /// </summary>
    internal IReadOnlyList<(IFilterDefinition filter, LogicConnector connector, bool negate)> Items => _items;

    public SearchCriteria() { }

    public SearchCriteria(IFilterDefinition initialFilter) =>
        _items.Add((initialFilter ?? throw new ArgumentNullException(nameof(initialFilter)), LogicConnector.And, false));

    public SearchCriteria(IEnumerable<IFilterDefinition> filters)
    {
        ArgumentNullException.ThrowIfNull(filters);
        foreach (var filter in filters)
        {
            ArgumentNullException.ThrowIfNull(filter, "Null filter found in the list.");
            _items.Add((filter, LogicConnector.And, false));
        }
    }

    public ISearchCriteria And(IFilterDefinition filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        _items.Add((filter, LogicConnector.And, false));
        return this;
    }

    public ISearchCriteria And(params IFilterDefinition[] filters)
    {
        if (filters is null || filters.Length == 0)
            throw new ArgumentException("Must provide at least one filter.", nameof(filters));

        foreach (var filter in filters)
        {
            ArgumentNullException.ThrowIfNull(filter, "Null filter found in the list.");
            _items.Add((filter, LogicConnector.And, false));
            Console.WriteLine($"_items.Count={_items.Count}");
        }
        return this;
    }

    public ISearchCriteria Or(IFilterDefinition filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        if (_items.Count == 0)
            throw new InvalidOperationException("Cannot call .Or(...) first when no filters exist yet. " +
                "Please call .And(...) for the first filter or use a constructor that sets an initial filter.");

        _items.Add((filter, LogicConnector.Or, false));
        Console.WriteLine($"_items.Count={_items.Count}");
        return this;
    }

    public ISearchCriteria Not(IFilterDefinition filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        _items.Add((filter, LogicConnector.And, true));
        return this;
    }

    public void SetParameterContext(IParameterContext paramContext) =>
        _paramContext = paramContext;

    public bool Matches<T>(T candidate, IParameterContext? paramCtx = null) =>
        SearchCriteriaExtensions.Matches(this, candidate, paramCtx);

    public Expression<Func<T, bool>> Build<T>() => BuildCore<T>(_paramContext);

    public Expression<Func<T, bool>> Build<T>(IParameterContext paramCtx) => BuildCore<T>(paramCtx);

    public override string ToString()
    {
        if (_items.Count == 0)
            return "(empty)";

        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < _items.Count; i++)
        {
            var (filter, connector, negate) = _items[i];
            if (i > 0)
                sb.Append($" {(connector == LogicConnector.And ? "AND" : "OR")} ");
            if (negate)
                sb.Append("NOT(");
            sb.Append($"{filter.PropertyName} {DescribeOperator(filter.Operator)} '{filter.Values[0]}'");
            if (negate)
                sb.Append(")");
        }
        return sb.ToString();
    }

    private string DescribeOperator(FilterOperator op) =>
        op switch
        {
            FilterOperator.Equal => "==",
            FilterOperator.NotEqual => "!=",
            FilterOperator.GreaterThan => ">",
            FilterOperator.GreaterOrEqual => ">=",
            FilterOperator.LessThan => "<",
            FilterOperator.LessOrEqual => "<=",
            FilterOperator.Has => "HAS",
            _ => op.ToString()
        };

    private Expression<Func<T, bool>> BuildCore<T>(IParameterContext? localContext)
    {
        if (_items.Count == 0)
            throw new NoFilterDefinitionFoundException("No filters found. Please add at least one filter.");

        var param = Expression.Parameter(typeof(T), "x");
        Expression body = Expression.Constant(true);

        foreach (var (filter, connector, negate) in _items)
        {
            var subExpr = BuildSubExpression<T>(param, filter, localContext);
            if (negate)
                subExpr = Expression.Not(subExpr);

            body = connector switch
            {
                LogicConnector.And => Expression.AndAlso(body, subExpr),
                LogicConnector.Or => Expression.OrElse(body, subExpr),
                _ => body
            };
        }

        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    private Expression BuildSubExpression<T>(ParameterExpression param, IFilterDefinition filter, IParameterContext? paramCtx)
    {
        object rawValue = filter.Values[0];
        if (rawValue is string s && s.StartsWith("$"))
        {
            if (paramCtx is null)
                throw new InvalidOperationException($"A placeholder '{s}' was encountered, but no parameter context was provided.");
            rawValue = paramCtx.ResolvePlaceholder(s);
        }
        var tmpFilter = new InMemoryResolvedFilter(filter.PropertyName, filter.Operator, rawValue);
        return BuildSubExpressionCore<T>(param, tmpFilter);
    }

    private Expression BuildSubExpressionCore<T>(ParameterExpression param, IFilterDefinition filter) =>
        filter.Operator != FilterOperator.Has
            ? BuildNonHasExpression<T>(param, filter)
            : filter.PropertyName.StartsWith("[")
                ? BuildHasForList<T>(param, filter.PropertyName, filter.Values[0])
                : BuildHasForSingleObject<T>(param, filter.PropertyName, filter.Values[0]);

    private Expression BuildNonHasExpression<T>(ParameterExpression param, IFilterDefinition filter)
    {
        var propInfo = typeof(T).GetProperty(filter.PropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new ArgumentException($"Property '{filter.PropertyName}' not found on type {typeof(T).Name}.");

        var leftExpr = Expression.Property(param, propInfo);
        object convertedVal = Convert.ChangeType(filter.Values[0], propInfo.PropertyType);
        var constExpr = Expression.Constant(convertedVal, propInfo.PropertyType);

        return filter.Operator switch
        {
            FilterOperator.Equal => Expression.Equal(leftExpr, constExpr),
            FilterOperator.NotEqual => Expression.NotEqual(leftExpr, constExpr),
            FilterOperator.GreaterThan => Expression.GreaterThan(leftExpr, constExpr),
            FilterOperator.GreaterOrEqual => Expression.GreaterThanOrEqual(leftExpr, constExpr),
            FilterOperator.LessThan => Expression.LessThan(leftExpr, constExpr),
            FilterOperator.LessOrEqual => Expression.LessThanOrEqual(leftExpr, constExpr),
            _ => throw new NotImplementedException($"Operator '{filter.Operator}' not implemented.")
        };
    }

    private Expression BuildHasForList<T>(ParameterExpression param, string fullPropertyName, object rawValue)
    {
        int closeBracketIdx = fullPropertyName.IndexOf(']');
        if (closeBracketIdx < 1)
            throw new ArgumentException($"Invalid bracket syntax for '{fullPropertyName}'. Missing ']'?");

        string listPropName = fullPropertyName.Substring(1, closeBracketIdx - 1);
        if (closeBracketIdx == fullPropertyName.Length - 1)
            return BuildHasListItemDirect<T>(param, listPropName, rawValue);

        int dotIdx = closeBracketIdx + 1;
        if (dotIdx >= fullPropertyName.Length || fullPropertyName[dotIdx] != '.')
            throw new ArgumentException($"Invalid bracket syntax for '{fullPropertyName}'. Must have a dot after brackets.");

        string subPropName = fullPropertyName.Substring(dotIdx + 1);
        if (string.IsNullOrEmpty(subPropName))
            throw new ArgumentException($"No subproperty found after brackets in '{fullPropertyName}'.");
        var listPropInfo = typeof(T).GetProperty(listPropName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new ArgumentException($"Property '{listPropName}' not found on type {typeof(T).Name} (bracket expression).");

        return BuildHasNestedExpression(param, listPropInfo, subPropName, rawValue);
    }

    private Expression BuildHasListItemDirect<T>(ParameterExpression param, string listPropName, object rawValue)
    {
        var listPropInfo = typeof(T).GetProperty(listPropName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new ArgumentException($"Property '{listPropName}' not found on type {typeof(T).Name}.");

        var leftExpr = Expression.Property(param, listPropInfo);
        var propType = listPropInfo.PropertyType;
        if (!propType.IsGenericType || propType.GetGenericTypeDefinition() != typeof(List<>))
            throw new ArgumentException($"Property '{listPropName}' is not a List<T> but used with bracket syntax.");

        var elementType = propType.GetGenericArguments()[0];
        var itemParam = Expression.Parameter(elementType, "item");
        object convertedVal = Convert.ChangeType(rawValue, elementType);
        var constExpr = Expression.Constant(convertedVal, elementType);
        var eqExpr = Expression.Equal(itemParam, constExpr);
        var lambda = Expression.Lambda(eqExpr, itemParam);
        var anyMethod = typeof(Enumerable).GetMethods()
            .FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 2)
            ?? throw new NotImplementedException("Could not find Enumerable.Any<T>(...) method.");
        var anyGeneric = anyMethod.MakeGenericMethod(elementType);
        var anyCall = Expression.Call(anyGeneric, leftExpr, lambda);
        var notNullExpr = Expression.NotEqual(leftExpr, Expression.Constant(null, propType));
        return Expression.AndAlso(notNullExpr, anyCall);
    }

    private Expression BuildHasForSingleObject<T>(ParameterExpression param, string fullPropertyName, object rawValue)
    {
        int dotCount = fullPropertyName.Count(c => c == '.');
        if (dotCount > 1)
            throw new NotImplementedException("Multiple dots not supported.");

        int dotIdx = fullPropertyName.IndexOf('.');
        if (dotIdx < 0)
            throw new ArgumentException($"No dot found in '{fullPropertyName}' for single-object Has scenario.");

        string parentName = fullPropertyName.Substring(0, dotIdx);
        string childName = fullPropertyName.Substring(dotIdx + 1);
        var parentProp = typeof(T).GetProperty(parentName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new ArgumentException($"Property '{parentName}' not found on type {typeof(T).Name} (dot expression).");
        var childProp = parentProp.PropertyType.GetProperty(childName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new ArgumentException($"Property '{childName}' not found on type {parentProp.PropertyType.Name} (dot subproperty).");
        var parentExpr = Expression.Property(param, parentProp);
        var notNullExpr = Expression.NotEqual(parentExpr, Expression.Constant(null, parentProp.PropertyType));
        var childExpr = Expression.Property(parentExpr, childProp);
        object convertedVal = Convert.ChangeType(rawValue, childProp.PropertyType);
        var constExpr = Expression.Constant(convertedVal, childProp.PropertyType);
        var eqExpr = Expression.Equal(childExpr, constExpr);
        return Expression.AndAlso(notNullExpr, eqExpr);
    }

    private static Expression BuildHasNestedExpression(ParameterExpression param, PropertyInfo listProp, string subPropName, object rawValue)
    {
        if (subPropName.Contains('.'))
            throw new NotImplementedException("Multiple dots not supported.");

        var leftExpr = Expression.Property(param, listProp);
        var propType = listProp.PropertyType;
        if (!propType.IsGenericType || propType.GetGenericTypeDefinition() != typeof(List<>))
            throw new ArgumentException($"Property '{listProp.Name}' is not a generic List but used with dot for 'Has'.");

        var elementType = propType.GetGenericArguments()[0];
        var itemParam = Expression.Parameter(elementType, "x");
        var subProp = elementType.GetProperty(subPropName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new ArgumentException($"Property '{subPropName}' not found on type {elementType.Name} (dot notation).");
        var subLeft = Expression.Property(itemParam, subProp);
        object convertedVal = Convert.ChangeType(rawValue, subProp.PropertyType);
        var subRight = Expression.Constant(convertedVal, subProp.PropertyType);
        var equality = Expression.Equal(subLeft, subRight);
        var subLambda = Expression.Lambda(equality, itemParam);
        var anyMethod = typeof(Enumerable).GetMethods()
            .FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 2)
            ?? throw new NotImplementedException("Could not find Enumerable.Any<T>(...) method.");
        var anyGeneric = anyMethod.MakeGenericMethod(elementType);
        var anyCall = Expression.Call(anyGeneric, leftExpr, subLambda);
        var notNullExpr = Expression.NotEqual(leftExpr, Expression.Constant(null, listProp.PropertyType));
        return Expression.AndAlso(notNullExpr, anyCall);
    }

    public ISearchCriteria AndCriteria(ISearchCriteria other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return this;

        if (other is not SearchCriteria sc)
            throw new ArgumentException("Incompatible criteria type.", nameof(other));

        if (_items.Count == 0)
        {
            _items.AddRange(sc._items);
            return this;
        }

        bool isFirst = true;
        foreach (var (f, c, negate) in sc._items)
        {
            // Force the first filter from the other criteria to use AND.
            _items.Add((f, isFirst ? LogicConnector.And : c, negate));
            isFirst = false;
        }
        return this;
    }
}

/// <summary>
/// A simple wrapper used after placeholders are resolved, always storing a single value.
/// </summary>
internal class InMemoryResolvedFilter : IFilterDefinition
{
    public string PropertyName { get; }
    public FilterOperator Operator { get; }
    public object[] Values { get; }

    public InMemoryResolvedFilter(string propName, FilterOperator op, object singleValue)
    {
        PropertyName = propName;
        Operator = op;
        Values = [ singleValue ];
    }
}