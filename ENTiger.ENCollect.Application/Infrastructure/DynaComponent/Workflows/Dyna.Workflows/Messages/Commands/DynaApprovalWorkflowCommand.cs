using ENTiger.ENCollect;

namespace ENCollect.Dyna.Workflows;

/// <summary>
/// Generic “user clicked a button” message.  
/// Any workflow that surfaces UI actions emits this command; the base saga
/// validates it and, on success, turns it into a domain-specific command via
/// the workflow’s <c>ActionCommandMap</c>.
/// </summary>
public class DynaWorkflowTransitionCommand : FlexCommandBridge<FlexAppContextBridge>
{
    public string WorkflowInstanceId { get; set; }
   
    public string StepName { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public ActionType ActionType { get; set; }
    public Object Dto { get; set; }
}