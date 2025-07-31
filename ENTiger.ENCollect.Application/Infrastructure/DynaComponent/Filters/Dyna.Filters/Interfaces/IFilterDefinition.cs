namespace ENCollect.Dyna.Filters;

/// <summary>
/// A single, atomic condition (e.g. property == value).
/// </summary>
public interface IFilterDefinition
{
    /// <summary>
    /// The property name (e.g. "Branch", "Region", "ThresholdRecommendationAmount").
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// The operator (Equal, NotEqual, etc.).
    /// </summary>
    FilterOperator Operator { get; }

    /// <summary>
    /// One or more values used for the operator.
    /// E.g. [0..1] for Equal, NotEqual, etc. or multiple for In.
    /// </summary>
    object[] Values { get; }
}