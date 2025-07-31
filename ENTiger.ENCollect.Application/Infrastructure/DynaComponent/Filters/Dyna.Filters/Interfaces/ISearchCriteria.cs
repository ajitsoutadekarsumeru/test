using System.Linq.Expressions;

namespace ENCollect.Dyna.Filters
{
    /// <summary>
    /// Represents a collection of filter definitions (single-value operators only)
    /// with AND/OR/NOT logic, plus optional placeholder resolution.
    /// <para>
    /// Can build an expression via <see cref="Build{T}()"/> or <see cref="Build{T}(IParameterContext)"/>.
    /// It also supports a reverse-lookup on a single candidate 
    /// via <see cref="Matches{T}(T, IParameterContext)"/>.
    /// </para>
    /// </summary>
    public interface ISearchCriteria
    {
        /// <summary>
        /// Builds a final expression (lambda) using any previously set context 
        /// or no placeholders if none were set.
        /// </summary>
        /// <typeparam name="T">Entity type for which the expression is built.</typeparam>
        /// <returns>An expression <c>(T entity) => bool</c> representing all filters.</returns>
        Expression<Func<T, bool>> Build<T>();

        /// <summary>
        /// Builds a final expression (lambda), using the specified parameter context 
        /// for placeholder resolution.
        /// </summary>
        /// <typeparam name="T">Entity type for which the expression is built.</typeparam>
        /// <param name="paramCtx">
        /// Context holding placeholder values; if null, an exception might be thrown 
        /// if placeholders exist but cannot be resolved.
        /// </param>
        /// <returns>An expression <c>(T entity) => bool</c> representing all filters.</returns>
        Expression<Func<T, bool>> Build<T>(IParameterContext paramCtx);

        /// <summary>
        /// Adds a single filter with AND logic.
        /// </summary>
        /// <param name="filter">The filter definition to add.</param>
        /// <returns>This <see cref="ISearchCriteria"/> for chaining.</returns>
        ISearchCriteria And(IFilterDefinition filter);

        /// <summary>
        /// Adds multiple filters with AND logic in a single call.
        /// </summary>
        /// <param name="filters">A set of filter definitions.</param>
        /// <returns>This <see cref="ISearchCriteria"/> for chaining.</returns>
        ISearchCriteria And(params IFilterDefinition[] filters);

        /// <summary>
        /// Adds a single filter with OR logic. 
        /// Only valid if at least one filter already exists in the aggregator.
        /// </summary>
        /// <param name="filter">The filter definition to add.</param>
        /// <returns>This <see cref="ISearchCriteria"/> for chaining.</returns>
        ISearchCriteria Or(IFilterDefinition filter);

        /// <summary>
        /// Adds a single filter that is negated (NOT), combined via AND.
        /// i.e., effectively "AND NOT(filter)".
        /// </summary>
        /// <param name="filter">The filter definition to negate and add.</param>
        /// <returns>This <see cref="ISearchCriteria"/> for chaining.</returns>
        ISearchCriteria Not(IFilterDefinition filter);

        /// <summary>
        /// Assigns a parameter context, which can be used for resolving
        /// any placeholder values in the filters.
        /// </summary>
        /// <param name="paramContext">The parameter context to store.</param>
        void SetParameterContext(IParameterContext paramContext);

        /// <summary>
        /// Checks if a single <paramref name="candidate"/> matches
        /// all stored filter conditions in this <see cref="ISearchCriteria"/>.
        /// <para>
        /// Internally, it compiles the expression (via <see cref="Build{T}"/>)
        /// and evaluates <paramref name="candidate"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The candidate object's type.</typeparam>
        /// <param name="candidate">A single object to test for inclusion.</param>
        /// <param name="paramCtx">
        /// Optional parameter context if placeholders exist. If null,
        /// uses any context previously set or throws if placeholders cannot be resolved.
        /// </param>
        /// <returns>
        /// True if <paramref name="candidate"/> satisfies all filters,
        /// false otherwise.
        /// </returns>
        bool Matches<T>(T candidate, IParameterContext? paramCtx = null);
    }
}