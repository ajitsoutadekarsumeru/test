namespace ENCollect.Dyna.Workflows;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// A fluent builder for creating a <see cref="DynaWorkflowDefinition{TContext}"/>.
///
/// Developers use this to define each step in a chain, then call <see cref="Build"/> to finalize.
/// </summary>
public class DynaWorkflowBuilder<TContext> where TContext : IContextDataPacket
{
    internal readonly List<DynaWorkflowStep<TContext>> _steps = new();
    internal DynaWorkflowStep<TContext>? _pendingStep;           // the step currently being configured


    private string _workflowName = string.Empty;
    private string _workflowDescription = string.Empty;
    private string _version = string.Empty;

    private readonly Dictionary<string, string> _metadata = new();

    /// <summary>
    /// Assign a name to the overall workflow.
    /// </summary>
    public DynaWorkflowBuilder<TContext> Named(string name)
    {
        _workflowName = name;
        return this;
    }

    /// <summary>
    /// Provide a longer description for the workflow, for developer or user reference.
    /// </summary>
    public DynaWorkflowBuilder<TContext> DescribedAs(string description)
    {
        _workflowDescription = description;
        return this;
    }

    /// <summary>
    /// Assign a version string to the workflow. This is required in final output.
    /// </summary>
    public DynaWorkflowBuilder<TContext> Version(string version)
    {
        _version = version;
        return this;
    }

    /// <summary>
    /// Add or override a metadata entry at the workflow level.
    /// </summary>
    public DynaWorkflowBuilder<TContext> WithMetadata(string key, string value)
    {
        _metadata[key] = value;
        return this;
    }

    /// <summary>
    /// Starts describing a new node in the graph.  
    /// Call <c>EndStep()</c> when you’re done adding settings to this node.
    /// </summary>
    public DynaStepBuilder<TContext> Step(string stepName)
    {
        if (_pendingStep != null)
            throw new InvalidOperationException("Previous step still open. Call EndStep() first.");

        if (string.IsNullOrWhiteSpace(stepName))
            throw new ArgumentException("stepName cannot be blank.", nameof(stepName));

        if (_steps.Any(s => s.StepName.Equals(stepName, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Step '{stepName}' already exists.");

        _pendingStep = new DynaWorkflowStep<TContext>
        {
            StepName = stepName,
            AllowedTransitions = new List<string>()
        };

        return new DynaStepBuilder<TContext>(this, _pendingStep);
    }

    /// <summary>
    /// Seals the definition into an immutable object that the runtime engine
    /// (sagas, navigators, UIs) can consume.
    /// </summary>
    public DynaWorkflowDefinition<TContext> Build()
    {
        // flush any still-open step
        if (_pendingStep != null)
        {
            _steps.Add(_pendingStep);
            _pendingStep = null;
        }

        var def = new DynaWorkflowDefinition<TContext>
        {
            WorkflowName = _workflowName,
            WorkflowDescription = _workflowDescription,
            Version = _version
        };

        // metadata copy
        foreach (var kvp in _metadata)
            def.Metadata[kvp.Key] = kvp.Value;

        // preserve author order
        def.Steps.AddRange(_steps);

        // 🔑 populate via helper (read-only property has no setter)
        foreach (var s in def.Steps)
            def.AddStepMapping(s.StepName, s);

        ValidateWorkflowDefinition(def);
        return def;
    }

    /// <summary>
    /// Guards against authoring mistakes (missing version, duplicate names, bad
    /// transition targets, etc.) so that runtime code never has to second-guess
    /// a definition.
    /// </summary>
    private void ValidateWorkflowDefinition(DynaWorkflowDefinition<TContext> definition)
    {
        if (string.IsNullOrWhiteSpace(definition.Version))
            throw new InvalidOperationException("Workflow must declare a Version.");

        if (!definition.Steps.Any())
            throw new InvalidOperationException("Workflow needs at least one step.");

        var dupNames = definition.Steps
                                 .GroupBy(s => s.StepName, StringComparer.OrdinalIgnoreCase)
                                 .Where(g => g.Count() > 1)
                                 .Select(g => g.Key);
        if (dupNames.Any())
            throw new InvalidOperationException($"Duplicate step names: {string.Join(", ", dupNames)}.");

        foreach (var step in definition.Steps)
        {
            //if (step.ActorFlow == null)
            //    throw new InvalidOperationException($"Step '{step.StepName}' is missing ActorFlow.");

            if (step.IsNeeded == null)
                throw new InvalidOperationException($"Step '{step.StepName}' is missing IsNeeded delegate.");
        }

        foreach (var step in definition.Steps)
        {
            if (!step.TransitionsDeclared)
                throw new InvalidOperationException(
                    $"Step '{step.StepName}' is missing WithTransitions(). " +
                    "Use .WithTransitions() with no args for a terminal step.");
        }

        var allNames = definition.Steps.Select(s => s.StepName)
                                       .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var badLinks = definition.Steps
            .SelectMany(s => s.AllowedTransitions.Select(t => (From: s.StepName, To: t)))
            .Where(link => !allNames.Contains(link.To));
        if (badLinks.Any())
            throw new InvalidOperationException(
                "AllowedTransitions reference unknown steps: " +
                string.Join(", ", badLinks.Select(l => $"{l.From}→{l.To}")));
    }
}