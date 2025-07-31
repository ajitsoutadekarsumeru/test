namespace ENCollect.Dyna.Filters
{
    /// <summary>
    /// A default dictionary-based implementation of <see cref="IParameterContext"/>.
    /// <para>
    /// Devs can call <see cref="Set"/> to supply placeholders, e.g.:
    /// <code>
    ///   var pc = new ParameterContext();
    ///   pc.Set("$SettlementAmount", 120m);
    /// </code>
    /// Then <see cref="ResolvePlaceholder"/> will return <c>120m</c> when the library
    /// encounters <c>"$SettlementAmount"</c> in a filter definition.
    /// </para>
    /// </summary>
    public class ParameterContext : IParameterContext
    {
        private readonly Dictionary<string, object> _map = new();
        /// <summary>
        /// Associates a placeholder name (e.g. <c>"$SettAmount"</c>)
        /// with an actual runtime value (e.g. <c>120m</c>).
        /// </summary>
        public void Set(string placeholderName, object value) => _map[placeholderName] = value;

        /// <inheritdoc />
        public object ResolvePlaceholder(string placeholderName) =>
            _map.TryGetValue(placeholderName, out var val)
                ? val
                : throw new ArgumentException($"No value found for placeholder '{placeholderName}'.");
    }
}