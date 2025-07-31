namespace ENCollect.Dyna.Workflows;

/// <summary>
/// Strategy that decides how a workflow advances from one step to the next,
/// plus a helper for force-jumping to an explicit target.
/// </summary>
/// <typeparam name="TContext">
/// The lightweight context object passed through the workflow. Must implement
/// <see cref="IContextDataPacket"/>.
/// </typeparam>
public interface IWorkflowNavigator<TContext> where TContext : IContextDataPacket
{
    /// <summary>
    /// Determines the next step to execute, given the workflow definition,
    /// the current step (or <c>null</c> when starting), and the runtime context.
    /// </summary>
    /// <param name="def">The immutable workflow definition.</param>
    /// <param name="currentStepName">
    /// The name of the step the saga is currently on, or <c>null</c> to fetch
    /// the first needed step at workflow start.</param>
    /// <param name="ctx">The runtime context used when evaluating <c>IsNeeded</c>.</param>
    /// <returns>
    /// The name of the next step to visit, or <c>null</c> if no further
    /// applicable steps remain (i.e., the workflow is complete).
    /// </returns>
    string? GetNext(
        DynaWorkflowDefinition<TContext> def,
        string? currentStepName,
        TContext ctx);

    /// <summary>
    /// Validates that <paramref name="stepName" /> exists in the workflow and
    /// returns it unchanged. Intended for “force-jump” scenarios where an
    /// external trigger moves the saga to a specific node.
    /// </summary>
    /// <param name="stepName">The target step’s unique name.</param>
    /// <param name="def">The workflow definition to validate against.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="stepName" /> does not exist in
    /// <paramref name="def" />.
    /// </exception>
    /// <returns>The validated <paramref name="stepName" />.</returns>
    string MoveToStep(string currentStepName,          
        string  targetStepName,
        DynaWorkflowDefinition<TContext> def);
}