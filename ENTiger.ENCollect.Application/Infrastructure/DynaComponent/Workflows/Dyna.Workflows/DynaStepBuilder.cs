namespace ENCollect.Dyna.Workflows;
/// <summary>
/// Provides fluent configuration for a single step in the workflow.
/// After configuring the step, call <see cref="Then"/> or <see cref="End"/> to finalize it.
/// </summary>
public class DynaStepBuilder<TContext> where TContext : IContextDataPacket
{
    private readonly DynaWorkflowBuilder<TContext> _parentBuilder;
    private readonly DynaWorkflowStep<TContext> _step;

    internal DynaStepBuilder(DynaWorkflowBuilder<TContext> parentBuilder, DynaWorkflowStep<TContext> step)
    {
        _parentBuilder = parentBuilder;
        _step = step;
    }

   

    /// <summary>
    /// Provide a longer textual description of this step's purpose.
    /// </summary>
    public DynaStepBuilder<TContext> DescribedAs(string description)
    {
        _step.StepDescription = description;
        return this;
    }

    /// <summary>
    /// Add or override a metadata entry at this step level.
    /// </summary>
    public DynaStepBuilder<TContext> WithMetadata(string key, string value)
    {
        _step.Metadata[key] = value;
        return this;
    }

    /// <summary>
    /// Sets the built-in button context for this step.
    /// Helps the front-end be dynamic about the action buttons they want to display
    /// </summary>
    public DynaStepBuilder<TContext> WithUIActionContext(UIActionContext ctx)
    {
        _step.UIActionContext = ctx;
        return this;
    }

    /// <summary>
    /// Assigns a function that determines if this step is needed (i.e., not skipped),
    /// given the lightweight TContext object.
    /// </summary>
    public DynaStepBuilder<TContext> IsNeeded(Func<TContext, bool> isNeededFunc)
    {
        _step.IsNeeded = isNeededFunc;
        return this;
    }

    /// <summary>
    /// Allows defining a cascading flow for "who" can act on this step.
    /// The <paramref name="configureFlow"/> callback configures the flow's conditions.
    /// </summary>
    public DynaStepBuilder<TContext> WithActorFlow(Action<ICascadingFlow<TContext>> configureFlow)
    {
        var flow = new CascadingFlow<TContext>();
        configureFlow?.Invoke(flow);

        _step.ActorFlow = flow;
        return this;
    }

    /// <summary>
    /// Declares which step-names this step can jump to next.  
    /// *Calling it with **no arguments** marks the step as a terminal node.*  
    /// Duplicate names (case-insensitive) are ignored.
    /// </summary>
    public DynaStepBuilder<TContext> WithTransitions(params string[]? targetStepNames)
    {
        if (targetStepNames is null)
            throw new ArgumentNullException(nameof(targetStepNames));

        _step.TransitionsDeclared = true;          // ✔ mark that author called it

        // Terminal step: explicit empty list
        if (targetStepNames.Length == 0)
        {
            _step.AllowedTransitions.Clear();
            return this;
        }

        foreach (var target in targetStepNames)
        {
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentException("Target names cannot be blank.", nameof(targetStepNames));

            if (!_step.AllowedTransitions.Any(t => t.Equals(target, StringComparison.OrdinalIgnoreCase)))
                _step.AllowedTransitions.Add(target);
        }
        return this;
    }

    /// <summary>
    /// Commits the configured node to the parent workflow and returns the parent
    /// so the author can start another <c>.Step(...)</c> or call <c>.Build()</c>.
    /// </summary>
    public DynaWorkflowBuilder<TContext> EndStep()
    {
        _parentBuilder._steps.Add(_step);
        _parentBuilder._pendingStep = null;
        return _parentBuilder;
    }


    

    /// <summary>
    /// Convenience for marking a terminal node (no AllowedTransitions).
    /// Reads nicely in the DSL: <c>.Step("Closed").End()</c>.
    /// Throws if you accidentally added transitions.
    /// </summary>
    public DynaWorkflowBuilder<TContext> End()
    {
        if (_step.AllowedTransitions.Any())
            throw new InvalidOperationException(
                $"Step '{_step.StepName}' has transitions defined; use EndStep() instead.");

        return EndStep();
    }
}