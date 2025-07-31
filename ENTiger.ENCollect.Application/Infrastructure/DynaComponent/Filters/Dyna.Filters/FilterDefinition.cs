namespace ENCollect.Dyna.Filters;

public class FilterDefinition(string propertyName, FilterOperator operatorType, object value) : IFilterDefinition
{
    public string PropertyName { get; } = Guard.Against.NullOrEmpty(propertyName, nameof(propertyName), "Property name cannot be empty.");
    public FilterOperator Operator { get; } = operatorType;
    public object[] Values { get; } = new[] { Guard.Against.Null(value, nameof(value)) };
}