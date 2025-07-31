namespace ENCollect.Dyna.Workflows;
/// <summary>
/// A container for all the steps that make up a single linear workflow.
/// Also holds descriptive metadata about the overall workflow.
/// </summary>
public class DynaWorkflowDefinition<TContext> where TContext : IContextDataPacket
{
    /// <summary>
    /// A logical name for the workflow (e.g., "Settlement Approval Flow").
    /// </summary>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// A longer description, explaining what this workflow does.
    /// </summary>
    public string WorkflowDescription { get; set; } = string.Empty;

    /// <summary>
    /// Required: a version string to support potential versioning of this workflow.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Arbitrary key-value metadata about the workflow as a whole.
    /// </summary>
    public Dictionary<string, string> Metadata { get; } = new();

    /// <summary>
    /// The ordered list of steps. The final step is assumed to be the last in the list
    /// or the one with the highest StepOrder.
    /// </summary>
    public List<DynaWorkflowStep<TContext>> Steps { get; } = new();

    /// <summary>
    /// Fast name → step map exposed for consumers that need random access
    /// (e.g., concrete sagas, monitoring tools).  Keys are case-insensitive.
    /// </summary>
    private readonly Dictionary<string, DynaWorkflowStep<TContext>> _stepLookup
        = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Builder-only helper that inserts (or overwrites) a mapping.
    /// Marked <c>internal</c> so external code cannot modify the dictionary.
    /// </summary>
    internal void AddStepMapping(string name, DynaWorkflowStep<TContext> step) =>
        _stepLookup[name] = step;

    public IReadOnlyDictionary<string, DynaWorkflowStep<TContext>> StepLookup => _stepLookup;

    /// <summary>Terse helper so saga code reads nicely.</summary>
    internal bool TryGetStep(string name, out DynaWorkflowStep<TContext>? step) =>
        StepLookup.TryGetValue(name, out step);

    public Dictionary<ActionType, Type> ActionCommandMap { get; set; } 
        = new Dictionary<ActionType, Type>();
}