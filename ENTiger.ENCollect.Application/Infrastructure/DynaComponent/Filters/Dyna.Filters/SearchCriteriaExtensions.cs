using System.Linq.Expressions;

namespace ENCollect.Dyna.Filters
{
    /// <summary>
    /// Provides an extension method for reverse lookup 
    /// (i.e., checking if a single candidate matches an ISearchCriteria).
    /// </summary>
    public static class SearchCriteriaExtensions
    {
        /// <summary>
        /// Checks if the specified <paramref name="candidate"/> 
        /// would be included by this criteria's conditions.
        /// <para>
        /// Internally, it calls <c>Build&lt;T&gt;()</c> (optionally with <paramref name="paramCtx"/>),
        /// compiles the resulting expression, and evaluates <paramref name="candidate"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The entity type to test (e.g., Recommender, ApprovalGrid, etc.).</typeparam>
        /// <param name="criteria">The aggregated criteria to test against.</param>
        /// <param name="candidate">A single object to check for a match.</param>
        /// <param name="paramCtx">
        /// Optional parameter context for placeholder resolution. If null, 
        /// the criteria's existing context (if any) will be used.
        /// </param>
        /// <returns>
        /// True if <paramref name="candidate"/> satisfies the criteria, 
        /// otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="criteria"/> or <paramref name="candidate"/> is null.</exception>
        public static bool Matches<T>(
            this ISearchCriteria criteria,
            T candidate,
            IParameterContext? paramCtx = null)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            if (candidate == null)
                throw new ArgumentNullException(nameof(candidate));

            // Build expression
            Expression<Func<T, bool>> expr;
            if (paramCtx != null)
                expr = criteria.Build<T>(paramCtx);
            else
                expr = criteria.Build<T>();

            // Compile and evaluate
            var fn = expr.Compile();
            return fn(candidate);
        }
    }
}