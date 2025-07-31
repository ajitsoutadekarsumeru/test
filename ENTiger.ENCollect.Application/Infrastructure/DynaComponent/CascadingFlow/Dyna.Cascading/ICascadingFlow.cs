namespace ENCollect.Dyna.Cascading
{
    /// <summary>
    /// Orchestrates multiple <see cref="IDynaCondition{TCascadingFlowDataPacket}"/> 
    /// in a linear, cascading manner. Each condition can decide if it applies
    /// by checking <c>IsMatch(...)</c> on the incoming data packet, and 
    /// if it does, merges the condition's <see cref="ISearchCriteria"/>.
    /// </summary>
    /// <typeparam name="TCascadingFlowDataPacket">
    /// A data packet or aggregate context type containing domain objects
    /// needed to evaluate each condition.
    /// </typeparam>
    public interface ICascadingFlow<TCascadingFlowDataPacket>
    {
        /// <summary>
        /// Adds one or more conditions to the flow in sequence.
        /// </summary>
        /// <param name="conditions">
        /// Conditions implementing <see cref="IDynaCondition{TCascadingFlowDataPacket}"/>.
        /// </param>
        void AddConditions(params IDynaCondition<TCascadingFlowDataPacket>[] conditions);

        /// <summary>
        /// Evaluates all added conditions against the specified data packet.
        /// For each condition that matches, its <see cref="ISearchCriteria"/>
        /// is merged (typically via AND). If <c>StopAfterMatch</c> is true,
        /// the process stops immediately after merging that condition.
        /// <para>
        /// The final merged <see cref="ISearchCriteria"/> can be used to build an expression
        /// or to reverse-lookup candidates. This aggregator is typically cached internally
        /// by the flow.
        /// </para>
        /// </summary>
        /// <param name="dataPacket">
        /// The domain data context passed to each condition's <c>IsMatch</c>.
        /// </param>
        /// <returns>
        /// A final <see cref="ISearchCriteria"/> aggregator representing matched conditions.
        /// </returns>
        ISearchCriteria Evaluate(TCascadingFlowDataPacket dataPacket);

        /// <summary>
        /// Reverse-lookup on a single <typeparamref name="TEntity"/> 
        /// using the aggregator produced by the last <see cref="Evaluate"/>.
        /// <para>
        /// If no <see cref="Evaluate"/> call has occurred yet, an exception is thrown.
        /// Otherwise, checks if the <paramref name="candidate"/> satisfies 
        /// the stored aggregator's criteria.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The candidate object's type.</typeparam>
        /// <param name="candidate">
        /// The single object to test for inclusion.
        /// </param>
        /// <param name="paramCtx">
        /// Optional parameter context for placeholder resolution. If null,
        /// the aggregator's existing context is used (if any).
        /// </param>
        /// <returns>
        /// True if <paramref name="candidate"/> matches all stored conditions; 
        /// false otherwise.
        /// </returns>
        bool IsIncluded<TEntity>(TEntity candidate, IParameterContext? paramCtx = null);
    }
}