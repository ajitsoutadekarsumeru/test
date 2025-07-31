namespace ENCollect.Dyna.Workflows;
/// <summary>
/// Specifies which action the user is performing 
/// on a given step: Approve, Reject or Negotiate.
/// The saga uses this to confirm step type vs. action type.
/// </summary>
public enum ActionEnum
{
    Approve,
    Reject,
    Recommend,
    Deny,
    Negotiate
}