namespace ENCollect.Dyna.Workflows;

/// <summary>
/// Default workflow-traversal strategy. 
/// It decides *which step name comes next*; it never mutates saga state.
/// </summary>
public sealed class StandardWorkflowNavigator<TContext> : IWorkflowNavigator<TContext>
    where TContext : IContextDataPacket
{
    /// <summary>
    /// Returns the next step in the workflow.  
    /// If <paramref name="currentStepName"/> is <c>null</c>,
    /// this is interpreted as “start the workflow” and the first needed
    /// step is selected.
    /// </summary>
    public string? GetNext(
        DynaWorkflowDefinition<TContext> def,
        string? currentStepName,
        TContext ctx)
    {
        var lookup = def.StepLookup;

        // — initial entry —
        if (string.IsNullOrWhiteSpace(currentStepName))
            return def.Steps.FirstOrDefault(s => s.IsNeeded(ctx))?.StepName;

        // — guard against corrupted saga state —
        if (!lookup.TryGetValue(currentStepName, out var cur))
            throw new InvalidOperationException($"Unknown current step '{currentStepName}'.");

        // internal note: breadth-first search over the transition graph
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var q       = new Queue<string>(cur.AllowedTransitions);

        while (q.Count > 0)
        {
            var name = q.Dequeue();
            if (!visited.Add(name)) continue;

            var step = lookup[name];
            if (step.IsNeeded(ctx)) return name;

            foreach (var child in step.AllowedTransitions)
                q.Enqueue(child);
        }
        return null;
    }

    /// <summary>
    /// Validates an explicit jump request (e.g., renegotiation success) and
    /// returns the confirmed target step name.
    /// </summary>
    public string MoveToStep(
        string currentStepName,
        string targetStepName,
        DynaWorkflowDefinition<TContext> def)
    {
        if (!def.StepLookup.ContainsKey(targetStepName))
            throw new InvalidOperationException($"Step '{targetStepName}' does not exist.");

        if (string.IsNullOrWhiteSpace(currentStepName))
            throw new InvalidOperationException("ForceMove cannot run before the workflow starts.");

        var curStep  = def.StepLookup[currentStepName];
        bool allowed = curStep.AllowedTransitions
                              .Any(t => t.Equals(targetStepName, StringComparison.OrdinalIgnoreCase));

        if (!allowed)
            throw new InvalidOperationException($"Illegal jump: '{currentStepName}' → '{targetStepName}'.");

        return targetStepName;
    }
}