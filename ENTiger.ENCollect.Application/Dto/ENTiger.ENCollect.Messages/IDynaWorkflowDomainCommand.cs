namespace ENTiger.ENCollect;


/// <summary>
/// Every domain command triggered by the workflow **must** implement this
/// so the saga can populate the common fields without reflection quirks.
/// </summary>
public interface IDynaWorkflowDomainCommand
{
    string   DomainId { get; set; }
    string UserId   { get; set; }
    string WorkflowInstanceId { get; set; } 
    string StepName { get; set; }
    string StepType { get; set; }

    /// <summary>The payload DTO for this command.</summary>
    object Dto { get; set; }
}