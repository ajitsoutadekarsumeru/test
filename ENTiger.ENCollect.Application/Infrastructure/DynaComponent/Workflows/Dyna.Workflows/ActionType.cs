namespace ENCollect.Dyna.Workflows;
/// <summary>
/// Specifies which action the user is performing 
/// on a given step: Recommend, Deny, Approve, or Reject.
/// The saga uses this to confirm step type vs. action type.
/// </summary>
public enum ActionType
{
    Recommend,
    Deny,
    Approve,
    Reject,
    CustomerAcceptance,
    CustomerReject,
    Renegotiate,
    Update,
    Cancel
}