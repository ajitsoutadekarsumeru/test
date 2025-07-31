namespace ENCollect.Dyna.Workflows;
/// <summary>
/// Represents a single step in the workflow definition.
/// Now referencing a lightweight "context" type rather than a full domain entity.
/// </summary>
public class DynaWorkflowStep<TContext> where TContext : IContextDataPacket
{
    /// <summary>
    /// The ordinal index/order of this step (1-based, 2-based, etc.).
    /// </summary>
    public int StepOrder { get; set; }

    /// <summary>
    /// Whether it's a Recommendation or Approval step.
    /// </summary>
    public UIActionContext UIActionContext { get; set; }

    /// <summary>Primary key inside the workflow; must be unique.</summary>
    public string StepName { get; set; } = string.Empty;   

    /// <summary>
    /// A longer description for this step. Helps with documentation.
    /// </summary>
    public string StepDescription { get; set; } = string.Empty;

    /// <summary>
    /// A function that determines if this step is needed for a given context
    /// (rather than a full domain object).
    /// </summary>
    public Func<TContext, bool> IsNeeded { get; set; } = _ => true;

    /// <summary>
    /// Defines how we determine "who" can act on this step,
    /// using the CascadingFlow library to produce filter logic.
    /// </summary>
    public ICascadingFlow<TContext> ActorFlow { get; set; } = null!;

    /// <summary>
    /// Arbitrary key-value metadata about this step.
    /// </summary>
    public Dictionary<string, string> Metadata { get; } = new();
    /// <summary>
    /// Outgoing edges in author-specified order.  
    /// The first target whose step evaluates <c>IsNeeded == true</c> is chosen.
    /// </summary>
    public List<string> AllowedTransitions { get; set; } = new();

    /// <summary>
    /// Set by the builder when the author calls <c>.WithTransitions()</c>.
    /// Used by the workflow validator to ensure the call is not forgotten.
    /// </summary>
    internal bool TransitionsDeclared { get; set; }
}