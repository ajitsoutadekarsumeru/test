namespace ENTiger.ENCollect;

/// <summary>
/// Represents a single top-level property or field that changed
/// (when comparing two objects).
/// </summary>
public record FieldChange
(
    // The name of the property or field that differs.
    string Field,
    // The old value of this property, converted to string.
    string Old,
    // The new value of this property, converted to string.
    string New
);