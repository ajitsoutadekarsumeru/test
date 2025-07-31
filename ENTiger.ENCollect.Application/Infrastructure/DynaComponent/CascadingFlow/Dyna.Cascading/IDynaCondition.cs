namespace ENCollect.Dyna.Cascading
{
    /// <summary>
    /// A domain-specific condition that, when <see cref="IsMatch"/> is true,
    /// contributes a full <see cref="ISearchCriteria"/> (with multiple filters)
    /// that will be merged (ANDed) into a final aggregator.
    /// <para>
    /// Typical usage: If <c>IsMatch(domainContext)</c> is true (e.g., 
    /// "principal waiver > 0"), we call <see cref="GetSearchCriteria()"/> 
    /// to retrieve the relevant filters (like principal-based conditions).
    /// </para>
    /// </summary>
    /// <typeparam name="TDomain">
    /// The domain context type containing data to decide if this condition applies 
    /// (e.g., principal/interest amounts, NPA flags, etc.).
    /// </typeparam>
    public interface IDynaCondition<TDomain>
    {
        /// <summary>
        /// Evaluates whether this condition is relevant, based on 
        /// the provided domain context.
        /// </summary>
        /// <param name="settlementAccountContext">
        /// A domain object containing data (e.g., settlement info).
        /// </param>
        /// <returns>
        /// True if we should apply this condition's filters, 
        /// or false if irrelevant.
        /// </returns>
        bool IsMatch(TDomain settlementAccountContext);

        /// <summary>
        /// Returns an <see cref="ISearchCriteria"/> containing one or more 
        /// filters that should be AND-merged if <see cref="IsMatch"/> is true.
        /// <para>
        /// E.g., if principal-based logic is relevant, we might define:
        /// (MinPrincipalWaiver <= $PrincipalWaiver) AND ($PrincipalWaiver <= MaxPrincipalWaiver).
        /// </para>
        /// </summary>
        /// <returns>
        /// An <see cref="ISearchCriteria"/> representing this condition's 
        /// filter definitions.
        /// </returns>
        ISearchCriteria GetSearchCriteria();

        /// <summary>
        /// If true, once we discover this condition is relevant and 
        /// merge its criteria, we stop evaluating further conditions 
        /// in the cascading flow.
        /// </summary>
        /// <example>
        /// "PrincipalCondition" might set this to true if we want 
        /// to exclude interest-based checks once principal is waived.
        /// </example>
        bool StopAfterMatch { get; }
    }
}