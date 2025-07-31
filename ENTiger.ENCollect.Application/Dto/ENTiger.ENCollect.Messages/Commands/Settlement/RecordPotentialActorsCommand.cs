

namespace ENTiger.ENCollect;
/// <summary>
/// A command used to update a read-model or external store
/// with the set of user IDs who can act on the newly selected step.
/// The base saga sends this automatically in MoveToNextNeededStep.
/// </summary>
public class RecordPotentialActorsCommand : FlexCommandBridge<FlexAppContextBridge>
{
    public string DomainId { get; set; }
    public string WorkflowName { get; set; }
    public string WorkflowInstanceId { get; set; }
  
    public string StepName { get; set; } // 
    public string StepType { get; set; }
    public List<string> EligibleUserIds { get; set; } = new();
    public string UIActionContext { get; set; }

}