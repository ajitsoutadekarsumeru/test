namespace ENCollect.Dyna.Workflows;
/// <summary>
/// Indicates if a workflow step is a Recommendation or an Approval.
/// </summary>
public enum UIActionContext
{
    None = 0,          // custom handling or no buttons
    Recommendation,    // “Recommend” / “Deny”
    Approval,           // “Approve”   / “Reject”
    Renegotiate,
    Acceptance
}