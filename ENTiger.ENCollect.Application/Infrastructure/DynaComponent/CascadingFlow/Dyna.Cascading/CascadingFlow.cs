using Ardalis.GuardClauses;

namespace ENCollect.Dyna.Cascading;

/// <summary>
/// Processes cascading conditions for a data packet by merging matching search criteria.
/// </summary>
/// <typeparam name="TCascadingFlowDataPacket">The type of the data packet used in condition evaluation.</typeparam>
public class CascadingFlow<TCascadingFlowDataPacket> : ICascadingFlow<TCascadingFlowDataPacket>
    where TCascadingFlowDataPacket : IContextDataPacket
{
    private readonly List<IDynaCondition<TCascadingFlowDataPacket>> _conditions = new();
    private ISearchCriteria _lastAggregator = null!;

    /// <inheritdoc />
    public void AddConditions(params IDynaCondition<TCascadingFlowDataPacket>[] conditions)
    {
        Guard.Against.Null(conditions, nameof(conditions));
        if (conditions.Length == 0) return;
        _conditions.AddRange(conditions);
    }

    /// <inheritdoc />
    public ISearchCriteria Evaluate(TCascadingFlowDataPacket dataPacket)
    {
        Guard.Against.Null(dataPacket, nameof(dataPacket));

        var overall = new SearchCriteria();
        foreach (var condition in _conditions)
        {
            if (condition.IsMatch(dataPacket))
            {
                overall.AndCriteria(condition.GetSearchCriteria());
                if (condition.StopAfterMatch)
                    break;
            }
        }
        _lastAggregator = overall;
        return overall;
    }

    /// <inheritdoc />
    public bool IsIncluded<TEntity>(TEntity candidate, IParameterContext? paramCtx = null) =>
        _lastAggregator is null
            ? throw new InvalidOperationException("No aggregator available. Call Evaluate(...) first.")
            : _lastAggregator.Matches(candidate, paramCtx);
}