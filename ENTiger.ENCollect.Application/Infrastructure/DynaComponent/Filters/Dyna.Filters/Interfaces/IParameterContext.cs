namespace ENCollect.Dyna.Filters
{
    /// <summary>
    /// Represents a parameter context for resolving placeholder values
    /// (e.g., <c>"$SettlementAmount"</c>) during expression building.
    /// <para>
    /// Your implementation typically uses a dictionary or other lookup
    /// to map placeholder names to actual runtime values.
    /// </para>
    /// </summary>
    public interface IParameterContext
    {
        /// <summary>
        /// Returns the runtime object for a given placeholder name.
        /// <para>
        /// For example, if the filter definition references <c>"$SettAmount"</c>,
        /// you look up <c>"$SettAmount"</c> here and return <c>120m</c> or whichever real value.
        /// </para>
        /// </summary>
        /// <param name="placeholderName">A string starting with <c>"$"</c> (by convention).</param>
        /// <returns>The resolved value. Must not be null or mismatch the property type.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="placeholderName"/> is not found in your context.
        /// </exception>
        object ResolvePlaceholder(string placeholderName);
    }
}