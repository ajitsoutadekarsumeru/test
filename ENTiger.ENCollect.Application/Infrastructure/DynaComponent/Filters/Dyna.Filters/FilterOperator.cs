namespace ENCollect.Dyna.Filters;

/// <summary>
/// Operators supporting typical single-value comparisons.
/// YAGNI principle: we removed multi-value operators like Between and In.
/// Now each operator only uses exactly one value.
/// </summary>
public enum FilterOperator
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterOrEqual,
    LessThan,
    LessOrEqual,

    // 'Between' and 'In' removed to keep the library simpler.
    // If needed in the future, we can reintroduce them.
    //Between,
    //In,

    // 'Has' still uses 1 value for list-of-primitives or dot-based logic.
    Has
}